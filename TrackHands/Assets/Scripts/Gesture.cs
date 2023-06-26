using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gesture 
{
    private string name;
    private string size;
    private Material mat;
    private string complexity;



    public Gesture(int n, string s, Material m) {
        name = "G"+n;
        size = s;
        mat = m;
        if (n < 5) {
            complexity = CopyImgHandler.SIMPLE;
        } else if (n > 8) {
            complexity = CopyImgHandler.COMPLEX;
        } else {
            complexity = CopyImgHandler.MEDIUM;
        }
    }

    public string GetSize() {
        return size;
    }

    public string GetName() {
        return name;
    }

   public Material GetMaterial() {
   		return mat;
   }

   public string GetComplexity() {
        return complexity;
   }

}
