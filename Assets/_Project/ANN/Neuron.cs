using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neuron {

    #region PROPERTIES
    public int NumInputs { get; private set; }
    public double Bias { get; set; }
    public double Output { get; set; }
    public double ErrorGradient { get; set; }

    public List<double> Inputs { get; private set; } = new List<double>();
    public List<double> Weights { get; private set; } = new List<double>(); 
    #endregion

    public Neuron(int nInputs)
	{
		float weightRange = (float) 2.4/(float) nInputs;
		Bias = UnityEngine.Random.Range(-weightRange,weightRange);
		NumInputs = nInputs;

		for(int i = 0; i < nInputs; i++)
			Weights.Add(UnityEngine.Random.Range(-weightRange,weightRange));
	}
}
