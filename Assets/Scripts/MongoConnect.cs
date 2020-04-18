﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using MongoDB.Bson;
using System;
using System.IO;
using System.Collections.Generic;

public class MongoConnect : MonoBehaviour
{

	private const string MONGO_URI = "mongodb+srv://admin:NOx030JUIpaHsMb0@inglesar-mmx7i.mongodb.net/test?retryWrites=true&w=majority";
	private const string DATABASE_NAME = "teste";
	private MongoClient client;
	private IMongoDatabase db;

	private IMongoCollection<BsonDocument> userCollection;


	//GridFS
	IGridFSBucket bucket;
	ObjectId id;
	Stream destination;
	

	private void Start()
	{
		try
		{
			client = new MongoClient(MONGO_URI);
			db = client.GetDatabase(DATABASE_NAME);
			print("CONECTADO");
			
		}
		catch (Exception e)
		{
			print("ERRO " + e);
		}

		userCollection = db.GetCollection<BsonDocument>("modelos.file");

		copyStream(@"C:\Users\gabri\Downloads");

	}
	
	public void insert() {

		Modelos3D newModel = new Modelos3D();
		newModel.nome = "Orange";
		
		var document = new BsonDocument { { "filename", "Modelo3D_cachorro" } };

		userCollection.InsertOne(document);
			

		print("INSERIDO");
	}

	public void getFilter() {

		userCollection = db.GetCollection<BsonDocument>("colecao1.files");
		var filter = Builders<BsonDocument>.Filter.Eq("filename", "Audio_cachorro");
		var studentDocument = userCollection.Find(filter).FirstOrDefault();
		print(studentDocument.ToString());


		//Modelos3D modelUser = userCollection.Find(user => user._id.Equals(id)).SingleOrDefault();
	}

	public void getCollection() {

		var documents = userCollection.Find(new BsonDocument()).ToList();

		foreach (BsonDocument doc in documents)
		{
			print(doc.ToString());
		}
	}

	public async void downloadingAsync(){

		id = new ObjectId("5e6a1f481adf7a38446aafe6");

		//DESTINATION ???????????????????????????

		await bucket.DownloadToStreamAsync(id, destination);

	}

	public static Stream getFileStream(IMongoDatabase db, string bucketName, string fileId)
	{
		MemoryStream stream = new MemoryStream();
		GridFSBucketOptions options = new GridFSBucketOptions();
		options.BucketName = bucketName;
		var bucket = new GridFSBucket(db, options);
		bucket.DownloadToStream(new ObjectId(fileId), stream);
		return stream;
	}

	public void copyStream(string destPath) {

		Stream stream = getFileStream(db, "colecao1", "5e6a1f481adf7a38446aafe6");

		try
		{
			/*
			using (var fileStream = new FileStream(destPath, FileMode.Create, FileAccess.Write))
			{
				stream.CopyTo(fileStream);
			}*/
			

			print("FOI PORRA");
		}
		catch (Exception e) {
			print(e);
		}

	}

}
