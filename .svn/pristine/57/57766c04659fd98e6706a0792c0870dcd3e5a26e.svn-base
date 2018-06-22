/*
   3DS Loader for repetier  
   Copyright 2013 J.F. van Leur

   Copyright 2011 repetier repetierdev@gmail.com

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace _3DPrinter.model
{
    
    class _3DSLoader
    {

        public class _3DScene
        {
            private readonly List<_3DObject> _objects = new List<_3DObject>();

            public _3DObject CreateObject(String name)
            {
                var result = new _3DObject { Name = name };
                _objects.Add(result);
                return result;
            }

            public int GetObjectCount()
            {
                return _objects.Count;
            }
            public _3DObject GetObject(int index)
            {
                return index >= _objects.Count
                    ? null
                    : _objects.ElementAt(index);
            }
            public List<_3DObject> GetObjects()
            {
                return _objects;
            }

            internal void Clear()
            {
                _objects.Clear();
            }
        }
        public class _3DObject
        {
            private _3DVertex[] _vertices;
            private _3DFace[] _faces;
            private int _currentVertex;
            private int _currentFace;

            public void InitializeVertices(int count)
            {
                _vertices = new _3DVertex[count];
                _currentVertex = 0;
            }
            public void InitializeFaces(int count)
            {
                _faces = new _3DFace[count];
                _currentFace = 0;
            }
            public String Name
            {
                get;
                set;
            }
            public int GetVertexCount()
            {
                return _vertices != null ? _vertices.Length : 0;
            }
            public int GetFaceCount()
            {
                return _faces != null ? _faces.Length : 0;
            }

            internal void AppendVertex(float x, float y, float z)
            {
                if (_currentVertex >= _vertices.Length) return;
                _vertices[_currentVertex++] = new _3DVertex(x, y, z);
            }

            internal void AppendFace(ushort vertex1, ushort vertex2, ushort vertex3, ushort flags)
            {
                if (_currentFace >= _faces.Length) return;
                _faces[_currentFace++] = new _3DFace(vertex1, vertex2, vertex3, flags);
            }
            public _3DFace GetFace(int index)
            {
                return _faces[index];
            }
            public _3DVertex GetVertex(int index)
            {
                return _vertices[index];
            }
        }
        public class _3DVertex
        {
            public _3DVertex(float x, float y, float z)
            {
                X = x;
                Y = y;
                Z = z;
            }
            public float X
            {
                get;
                set;
            }
            public float Y
            {
                get;
                set;
            }
            public float Z
            {
                get;
                set;
            }
        }
        public class _3DFace
        {
            public _3DFace(ushort vertex1, ushort vertex2, ushort vertex3, ushort flags)
            {
                Vertex1 = vertex1;
                Vertex2 = vertex2;
                Vertex3 = vertex3;
                Flags = flags;
            }
            public ushort Vertex1
            {
                get;
                set;
            }
            public ushort Vertex2
            {
                get;
                set;
            }
            public ushort Vertex3
            {
                get;
                set;
            }
            public ushort Flags
            {
                get;
                set;
            }
        }
        private struct Chunk
        {
            public ushort ChunkId;
            public uint ChunkLength;
        }
        private struct Point
        {
            public float X, Y, Z;
        }
        private struct Face
        {
            public ushort Vertex1, Vertex2, Vertex3, Flags;
        }
        private struct Word
        {
            public ushort Value;
        }
        private struct DWord
        {
            public uint Value;
        }
       

        private Word ReadWord(BinaryReader reader)
        {
            Word result = new Word {
                Value = reader.ReadUInt16()
            };
            return result;
        }
        private DWord ReadDword(BinaryReader reader)
        {
            DWord result = new DWord {
                Value = reader.ReadUInt32()
            };
            return result;
        }
        private Chunk ReadChunk(BinaryReader reader)
        {
            Chunk result = new Chunk {
                ChunkId = ReadWord(reader).Value, 
                ChunkLength = ReadDword(reader).Value
            };
            return result;
        }
        private Point ReadPoint(BinaryReader reader)
        {
            Point result = new Point {
                X = reader.ReadSingle(),
                Y = reader.ReadSingle(),
                Z = reader.ReadSingle()
            };
            return result;
        }
        private Face ReadFace(BinaryReader reader)
        {
            Face result = new Face {
                Vertex1 = reader.ReadUInt16(),
                Vertex2 = reader.ReadUInt16(),
                Vertex3 = reader.ReadUInt16(),
                Flags = reader.ReadUInt16()
            };
            return result;

        }

        private _3DScene _scene;


        public _3DScene Load(String fileName)
        {
            _scene = new _3DScene();
            Read3DsFile(fileName);
            return _scene;
        }
        public void Clear()
        {
            _scene.Clear();
        }


        internal void Read3DsFile(string fileName)
        {

            // read chunks
            using (FileStream fs = 
                new FileStream(
                    fileName, 
                    FileMode.Open, 
                    FileAccess.Read, 
                    FileShare.Read))
            {
                using (BinaryReader bs = new BinaryReader(fs))
                {
                    _3DObject currentObject = null;

                    // read all chunks
                    long fileSize = fs.Length;
                    long bytesRead = 0;
                    bool meshMode = false;

                    const int chunkSize = sizeof(ushort) + sizeof(uint);


                    while (bytesRead < fileSize)
                    {
                        Boolean skipChunk = false;
                        Word word;

                        Chunk chunk = ReadChunk(bs);
                        bytesRead += chunkSize;

                        switch (chunk.ChunkId)
                        {
                            case 0x0000:
                                // NULL chunk
                                break;
                            case 0x3D3D:
                                // 3D editor chunk
                                meshMode = true;
                                // log("Mesh found");
                                break;
                            case 0x4000:
                                // object block
                                if (!meshMode)
                                {
                                    throw new FileLoadException("Invalid 3DS file, object block before mesh block");
                                }

                                // skip zero terminated object name
                                StringBuilder name = new StringBuilder();
                                int b;
                                while ((b = bs.ReadByte()) != 0)
                                {
                                    name.Append(Convert.ToChar(b));
                                    bytesRead++;
                                }
                                // create object
                                currentObject = _scene.CreateObject(name.ToString());

                                break;
                            case 0x4100:
                                // triangular mesh, informative                        
                                break;
                            case 0x4110:
                                // vertices list
                                // check if we have a current object
                                if (currentObject == null)
                                {
                                    currentObject = _scene.CreateObject("Unnamed Object " + _scene.GetObjectCount().ToString());
                                }
                                // check how many vertices to read

                                word = ReadWord(bs);
                                int pointsToRead = word.Value;

                                currentObject.InitializeVertices(pointsToRead);
                                // add points to current object
                                while (pointsToRead-- > 0)
                                {
                                    Point point = ReadPoint(bs);
                                    currentObject.AppendVertex(point.X, point.Y, point.Z);
                                }
                                // count bytesread
                                bytesRead += chunk.ChunkLength - chunkSize;
                                break;
                            case 0x4120:
                                // faces 
                                // check if we have a current object
                                if (currentObject == null)
                                {
                                    currentObject = _scene.CreateObject("");
                                }

                                word = ReadWord(bs);
                                int facesToRead = word.Value;
                                currentObject.InitializeFaces(facesToRead);

                                // add faces to current object
                                while (facesToRead-- > 0)
                                {
                                    Face face = ReadFace(bs);
                                    currentObject.AppendFace(face.Vertex1, face.Vertex2, face.Vertex3, face.Flags);
                                }

                                // count bytes read
                                bytesRead += chunk.ChunkLength - chunkSize;

                                break;
                            case 0x4160:
                                skipChunk = true;
                                break;
                            case 0x4D4D:
                                // file header
                                break;
                            default:
                                // seek to next chunk
                                skipChunk = true;
                                break;
                        }

                        // skip chunk
                        if (!skipChunk) continue;
                        // skip bytes
                        bs.ReadBytes((int)chunk.ChunkLength - chunkSize);
                        bytesRead += chunk.ChunkLength - chunkSize;
                    }
                    bs.Close();
                    fs.Close();
                }
            }

        }

    }
}
