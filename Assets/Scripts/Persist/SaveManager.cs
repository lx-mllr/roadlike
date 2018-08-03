using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager {

    BinaryFormatter formatter;

    public SaveManager () {
        formatter = new BinaryFormatter();
    }

    public void Save<T> (string fileName, T data) where T : struct {
        fileName = "/" + fileName;
        FileStream file = File.Create(Application.persistentDataPath + fileName);

        formatter.Serialize(file, data);
        file.Close();
    }

    public T Load<T> (string fileName) where T : struct {
        T t;
        fileName = "/" + fileName;

        if (File.Exists(Application.persistentDataPath + fileName)) {
            FileStream file = File.Open(Application.persistentDataPath + fileName, FileMode.Open);
            t = (T) formatter.Deserialize(file);
            file.Close();
        }
        else {
            t = new T();
        }
        
        return t;
    }
}