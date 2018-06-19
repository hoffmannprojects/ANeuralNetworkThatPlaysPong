using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANN
{
    //TODO: Change to properties or private fields.
	private int _numInputs;
	private int _numOutputs;
	private int _numHidden;
	private int _numNPerHidden;
	private double _alpha;

	List<Layer> layers = new List<Layer>();

	public ANN(int nI, int nO, int nH, int nPH, double a)
	{
		_numInputs = nI;
		_numOutputs = nO;
		_numHidden = nH;
		_numNPerHidden = nPH;
		_alpha = a;

		if(_numHidden > 0)
		{
			layers.Add(new Layer(_numNPerHidden, _numInputs));

			for(int i = 0; i < _numHidden-1; i++)
			{
				layers.Add(new Layer(_numNPerHidden, _numNPerHidden));
			}

			layers.Add(new Layer(_numOutputs, _numNPerHidden));
		}
		else
		{
			layers.Add(new Layer(_numOutputs, _numInputs));
		}
	}

	public List<double> Train(List<double> inputValues, List<double> desiredOutput)
	{
		List<double> outputValues = new List<double>();
		outputValues = CalcOutput(inputValues, desiredOutput);
		UpdateWeights(outputValues, desiredOutput);
		return outputValues;
	}

	public List<double> CalcOutput(List<double> inputValues, List<double> desiredOutput)
	{
		List<double> inputs = new List<double>();
		List<double> outputValues = new List<double>();
		int currentInput = 0;

		if(inputValues.Count != _numInputs)
		{
			Debug.Log("ERROR: Number of Inputs must be " + _numInputs);
			return outputValues;
		}

		inputs = new List<double>(inputValues);
		for(int i = 0; i < _numHidden + 1; i++)
		{
				if(i > 0)
				{
					inputs = new List<double>(outputValues);
				}
				outputValues.Clear();

				for(int j = 0; j < layers[i].numNeurons; j++)
				{
					double N = 0;
					layers[i].neurons[j].Inputs.Clear();

					for(int k = 0; k < layers[i].neurons[j].NumInputs; k++)
					{
					    layers[i].neurons[j].Inputs.Add(inputs[currentInput]);
						N += layers[i].neurons[j].Weights[k] * inputs[currentInput];
						currentInput++;
					}

					N -= layers[i].neurons[j].Bias;

					if(i == _numHidden)
						layers[i].neurons[j].Output = ActivationFunctionO(N);
					else
						layers[i].neurons[j].Output = ActivationFunction(N);
					
					outputValues.Add(layers[i].neurons[j].Output);
					currentInput = 0;
				}
		}
		return outputValues;
	}

	public string PrintWeights()
	{
		string weightStr = "";
		foreach(Layer l in layers)
		{
			foreach(Neuron n in l.neurons)
			{
				foreach(double w in n.Weights)
				{
					weightStr += w + ",";
				}
			}
		}
		return weightStr;
	}

	public void LoadWeights(string weightStr)
	{
		if(weightStr == "") return;
		string[] weightValues = weightStr.Split(',');
		int w = 0;
		foreach(Layer l in layers)
		{
			foreach(Neuron n in l.neurons)
			{
				for(int i = 0; i < n.Weights.Count; i++)
				{
					n.Weights[i] = System.Convert.ToDouble(weightValues[w]);
					w++;
				}
			}
		}
	}
	
	void UpdateWeights(List<double> outputs, List<double> desiredOutput)
	{
		double error;
		for(int i = _numHidden; i >= 0; i--)
		{
			for(int j = 0; j < layers[i].numNeurons; j++)
			{
				if(i == _numHidden)
				{
					error = desiredOutput[j] - outputs[j];
					layers[i].neurons[j].ErrorGradient = outputs[j] * (1-outputs[j]) * error;
				}
				else
				{
					layers[i].neurons[j].ErrorGradient = layers[i].neurons[j].Output * (1-layers[i].neurons[j].Output);
					double errorGradSum = 0;
					for(int p = 0; p < layers[i+1].numNeurons; p++)
					{
						errorGradSum += layers[i+1].neurons[p].ErrorGradient * layers[i+1].neurons[p].Weights[j];
					}
					layers[i].neurons[j].ErrorGradient *= errorGradSum;
				}	
				for(int k = 0; k < layers[i].neurons[j].NumInputs; k++)
				{
					if(i == _numHidden)
					{
						error = desiredOutput[j] - outputs[j];
						layers[i].neurons[j].Weights[k] += _alpha * layers[i].neurons[j].Inputs[k] * error;
					}
					else
					{
						layers[i].neurons[j].Weights[k] += _alpha * layers[i].neurons[j].Inputs[k] * layers[i].neurons[j].ErrorGradient;
					}
				}
				layers[i].neurons[j].Bias += _alpha * -1 * layers[i].neurons[j].ErrorGradient;
			}

		}

	}

	double ActivationFunction(double value)
	{
		return TanH(value);
	}

	double ActivationFunctionO(double value)
	{
		return TanH(value);
	}

	double TanH(double value)
	{
		double k = (double) System.Math.Exp(-2*value);
    	return 2 / (1.0f + k) - 1;
	}

	double Sigmoid(double value) 
	{
    	double k = (double) System.Math.Exp(value);
    	return k / (1.0f + k);
	}
}
