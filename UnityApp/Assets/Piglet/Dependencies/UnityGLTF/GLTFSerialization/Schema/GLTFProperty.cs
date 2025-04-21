using System;
using System.Collections.Generic;
using Piglet.GLTF.Extensions;
using Piglet.Newtonsoft.Json;
using Piglet.Newtonsoft.Json.Linq;

namespace Piglet.GLTF.Schema
{
	public class GLTFProperty
	{
		private static Dictionary<string, ExtensionFactory> _extensionRegistry = new Dictionary<string, ExtensionFactory>();
		private static DefaultExtensionFactory _defaultExtensionFactory = new DefaultExtensionFactory();
		private static KHR_materials_pbrSpecularGlossinessExtensionFactory _KHRExtensionFactory = new KHR_materials_pbrSpecularGlossinessExtensionFactory();
		private static KHR_materials_unlitExtensionFactory _KHR_materials_unlitFactory = new KHR_materials_unlitExtensionFactory();
		private static KHR_materials_variantsExtensionFactory _KHR_materials_variantsFactory = new KHR_materials_variantsExtensionFactory();
		private static KHR_texture_basisuExtensionFactory _KHR_texture_basisuFactory = new KHR_texture_basisuExtensionFactory();
		private static KHR_draco_mesh_compressionExtensionFactory _KHR_draco_mesh_compressionFactory = new KHR_draco_mesh_compressionExtensionFactory();
		private static KHR_texture_transformExtensionFactory _TexTransformFactory = new KHR_texture_transformExtensionFactory();

		public static void RegisterExtension(ExtensionFactory extensionFactory)
		{
			_extensionRegistry.Add(extensionFactory.ExtensionName, extensionFactory);
		}

		public Dictionary<string, Extension> Extensions;
		public JToken Extras;

		public void DefaultPropertyDeserializer(GLTFRoot root, JsonReader reader)
		{
			switch (reader.Value.ToString())
			{
				case "extensions":
					Extensions = DeserializeExtensions(root, reader);
					break;
				case "extras":
					// advance to property value
					reader.Read();
					if (reader.TokenType != JsonToken.StartObject)
						throw new Exception(string.Format("extras must be an object at: {0}", reader.Path));
					Extras = JToken.ReadFrom(reader);
					break;
				default:
					SkipValue(reader);
					break;
			}
		}

		private void SkipValue(JsonReader reader)
		{
			if (!reader.Read())
			{
				throw new Exception("No value found.");
			}

			if (reader.TokenType == JsonToken.StartObject)
			{
				SkipObject(reader);
			}
			else if (reader.TokenType == JsonToken.StartArray)
			{
				SkipArray(reader);
			}
		}

		private void SkipObject(JsonReader reader)
		{
			while (reader.Read() && reader.TokenType != JsonToken.EndObject) {
				if (reader.TokenType == JsonToken.StartArray)
				{
					SkipArray(reader);
				}
				else if (reader.TokenType == JsonToken.StartObject)
				{
					SkipObject(reader);
				}
			}
		}

		private void SkipArray(JsonReader reader)
		{
			while (reader.Read() && reader.TokenType != JsonToken.EndArray) {
				if (reader.TokenType == JsonToken.StartArray)
				{
					SkipArray(reader);
				}
				else if (reader.TokenType == JsonToken.StartObject)
				{
					SkipObject(reader);
				}
			}
		}

		private Dictionary<string, Extension> DeserializeExtensions(GLTFRoot root, JsonReader reader)
		{
			if (reader.Read() && reader.TokenType != JsonToken.StartObject)
			{
				throw new GLTFParseException("GLTF extensions must be an object");
			}

			JObject extensions = (JObject)JToken.ReadFrom(reader);
			var extensionsCollection = new Dictionary<string, Extension>();

			foreach(JToken child in extensions.Children())
			{
				if (child.Type != JTokenType.Property)
				{
					throw new GLTFParseException("Children token of extensions should be properties");
				}

				JProperty childAsJProperty = (JProperty) child;
				string extensionName = childAsJProperty.Name;
				ExtensionFactory extensionFactory;

				if (_extensionRegistry.TryGetValue(extensionName, out extensionFactory))
				{
					extensionsCollection.Add(extensionName, extensionFactory.Deserialize(root, childAsJProperty));
				}
				else if (extensionName.Equals(KHR_materials_pbrSpecularGlossinessExtensionFactory.EXTENSION_NAME))
				{
					extensionsCollection.Add(extensionName, _KHRExtensionFactory.Deserialize(root, childAsJProperty));
				}
				else if (extensionName.Equals(KHR_texture_basisuExtensionFactory.EXTENSION_NAME))
				{
					extensionsCollection.Add(extensionName, _KHR_texture_basisuFactory.Deserialize(root, childAsJProperty));
				}
				else if (extensionName.Equals(KHR_draco_mesh_compressionExtensionFactory.EXTENSION_NAME))
				{
					extensionsCollection.Add(extensionName, _KHR_draco_mesh_compressionFactory.Deserialize(root, childAsJProperty));
				}
				else if (extensionName.Equals(KHR_texture_transformExtensionFactory.EXTENSION_NAME))
				{
					extensionsCollection.Add(extensionName, _TexTransformFactory.Deserialize(root, childAsJProperty));
				}
				else if (extensionName.Equals(KHR_materials_unlitExtensionFactory.EXTENSION_NAME))
				{
					extensionsCollection.Add(extensionName, _KHR_materials_unlitFactory.Deserialize(root, childAsJProperty));
				}
				else if (extensionName.Equals(KHR_materials_variantsExtensionFactory.EXTENSION_NAME))
				{
					extensionsCollection.Add(extensionName, _KHR_materials_variantsFactory.Deserialize(root, childAsJProperty));
				}
				else
				{
					extensionsCollection.Add(extensionName, _defaultExtensionFactory.Deserialize(root, childAsJProperty));
				}
			}

			return extensionsCollection;
		}

		public virtual void Serialize(JsonWriter writer)
		{
			if (Extensions != null && Extensions.Count > 0)
			{
				writer.WritePropertyName("extensions");
				writer.WriteStartObject();
				foreach (var extension in Extensions)
				{
					JToken extensionToken = extension.Value.Serialize();
					extensionToken.WriteTo(writer);
				}
				writer.WriteEndObject();
			}

			if(Extras != null)
			{
				Extras.WriteTo(writer);
			}
		}
	}
}
