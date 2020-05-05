using System.Collections;
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

		//copyStream(@"C:\Users\gabri\Downloads");

	}

	public void insert() {

		Modelos3D newModel = new Modelos3D();
		newModel.nome = "Orange";

		var document = new BsonDocument { { "filename", "orange" } };

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

	public static Byte[] getFileBytes(IMongoDatabase db, string bucketName, string fileId) {
		GridFSBucketOptions options = new GridFSBucketOptions();
		options.BucketName = bucketName;
		var bucket = new GridFSBucket(db, options);
		return bucket.DownloadAsBytes(new ObjectId(fileId));
	}

	public void salvaFile() {

		string nome = "Audio_Cachorro";
		string nomeImg = "Textura_Cachorro";
		string nomeModel = "Model_Cachorro";

		var pathMp3 = @"C:\Users\gabri\Documents\Unity Projects\IC-AR`\Assets\" + nome + ".mp3";
		var pathImg = @"C:\Users\gabri\Documents\Unity Projects\IC-AR`\Assets\" + nomeImg + ".png";
		var pathModel = @"C:\Users\gabri\Documents\Unity Projects\IC-AR`\Assets\" + nomeModel + ".obj";

		try {
			var dataMp3 = getFileBytes(db, "colecao1", "5e6a1f481adf7a38446aafe6");
			var dataImg = getFileBytes(db, "colecao1", "5e6a1f481adf7a38446aafe5");
			var dataModel = getFileBytes(db, "colecao1", "5e6a1f481adf7a38446aafe8");

			File.WriteAllBytes(pathMp3, dataMp3);
			File.WriteAllBytes(pathImg, dataImg);
			File.WriteAllBytes(pathModel, dataModel);
		}
		catch (Exception e) {
			print("ERRO " + e);
		}	
	}
}
