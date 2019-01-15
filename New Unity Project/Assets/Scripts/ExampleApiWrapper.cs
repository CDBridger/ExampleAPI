using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ApiWrapper;

public class ExampleApiWrapper : MonoBehaviour
{
    public Text Text;

    //Start is called before the first frame update
    void Start()
    {
        Debug.Log("C++ Vector Class ----------------->");
        Text.text = "C++ Vector Class ----------------->";
        var cppVector = new ApiWrapper.Vector3(1, 2, 3);
        Debug.Log(cppVector);
        Text.text = cppVector.ToString();
        cppVector.MakeUnitVector();
        Debug.Log(cppVector);
        Text.text = cppVector.ToString();
        var otherVector = cppVector.GetUnitVector();
        Debug.Log($" Copied Vector : {otherVector}");
        Text.text = $" Copied Vector : {otherVector}";

        Debug.Log("C++ API Class ----------------->");
        Text.text = "C++ API Class ----------------->";

        var api = new API();
        Debug.Log("Inited: ");
        Text.text = "Inited: ";

        var collection = api.Vectors;
        foreach (var vec in collection)
        {
            Debug.Log(vec);
            Text.text = vec.ToString();
        }
        List<ApiWrapper.Vector3> sendingCollection;
        var rnd = new System.Random();
        for (int i = 0; i < 10; i++)
        {
            sendingCollection = new List<ApiWrapper.Vector3> {
                    new ApiWrapper.Vector3(rnd.Next(10), rnd.Next(10), rnd.Next(10)),
                    new ApiWrapper.Vector3(rnd.Next(10), rnd.Next(10), rnd.Next(10))
                };
            api.Vectors = sendingCollection;
            collection = api.Vectors;
            foreach (var vec in collection)
            {
                Debug.Log(vec);
                Text.text = vec.ToString();

            }

        }    }

    //Update is called once per frame
    void Update()
    {

    }
}
