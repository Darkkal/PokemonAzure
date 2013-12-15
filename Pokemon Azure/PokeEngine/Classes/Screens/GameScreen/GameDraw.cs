using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using System.IO;
using SD = System.Drawing;
using PokeEngine.Map;
using PC = PokeEngine.Trainers;
using PokeEngine.Menu;
using System.Threading.Tasks;
using PokeEngine.Trainers;


namespace PokeEngine.Screens
{
    public static class GameDraw
    {
        private static Rectangle spriteSheetSize = new Rectangle(0, 0, 116, 153);
        public static SortedList<String, Texture2D> groundTexture;
        private static GraphicsDeviceManager graphics;
        private static ContentManager contentManager;
        private static SpriteFont font;
        private static float aspectRatio;
        private static int numberOfTiles = 0;
        public static bool drawAdjObjects = false;

        private static Effect effect;
        private static Texture2D missingAssetTex;

        //World View Projection
        public static Matrix world = Matrix.Identity;
        public static Matrix view;
        public static Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f),
            aspectRatio, 1.0f, 10000.0f);

        //buffers for adjacent ground
        private static VertexBuffer vb;
        private static IndexBuffer ib;
        private static ushort[] tilesPerTex;
        private static SortedList<String, Texture2D> NPCTextures; //links npc texture names to the actual texture

        public static Vector3 cameraPosition;
        public static Vector3 cameraOffset = new Vector3(0.0f, 6.0f, 9.0f); //distance from player the camera is
        public static Vector3 playerPosition;

        private static CustomVertex[] playerVertices = new CustomVertex[4];
        private static short[] playerIndices = new short[6];


        #region initialization
        public static void Initialize(GraphicsDeviceManager g, ContentManager c, SpriteFont f)
        {
            graphics = g;
            contentManager = c;
            font = f;
            aspectRatio = (float)g.GraphicsDevice.Viewport.Width / (float)g.GraphicsDevice.Viewport.Height;
            groundTexture = new SortedList<string, Texture2D>();
            NPCTextures = new SortedList<String, Texture2D>();
            LoadTileTextures();
            //tileModel = c.Load<Model>(@"Models\tile");

            playerPosition = Vector3.Zero;
            cameraPosition = new Vector3(0.0f, 1500.0f, 1500.0f);

            graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;

            effect = c.Load<Effect>("Effect");
            missingAssetTex = c.Load<Texture2D>("debug_question_tile");
        }

        //loads tile textures
        public static void LoadTileTextures()
        {
            //empty the list of textures if possible
            groundTexture.Clear();

            string rootDir = AppDomain.CurrentDomain.BaseDirectory;
            //TEXTURES GO INTO \Content\Tiles and are loaded automatically, make sure to set "Copy Always"
            string texturesDir = Path.Combine(rootDir, "Content\\Tiles");

            foreach (string path in Directory.GetFiles(texturesDir))
            {
                if (path.ToLower().EndsWith(".png"))
                {
                    //SD is using the system drawing namespace
                    //SD.Bitmap image = new SD.Bitmap(path);
                    //SD.Graphics imageGraphics = SD.Graphics.FromImage(image);
                    String[] temp = path.Split('\\');
                    //Texture2D newTex = new Texture2D(graphics.GraphicsDevice, image.Width, image.Height);
                    Texture2D newTex = Texture2D.FromStream(graphics.GraphicsDevice, new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read));
                    groundTexture.Add(temp[temp.Length - 1], newTex);
                }
            }

        }
        #endregion //load textures and so on


        public static void MakeAdjBuffers(World inWorld)
        {
            //all vertexes for the ground will be in one big buffer
            //indexes will also be in one big buffer, using appropriate offsets

            //these are always the same, so make them now
            Vector3 normal = Vector3.Up;
            Vector2 TLTex = new Vector2(0f, 0f);
            Vector2 TRTex = new Vector2(1f, 0f);
            Vector2 BRTex = new Vector2(1f, 1f);
            Vector2 BLTex = new Vector2(0f, 1f);
            //calculate number of tiles
            numberOfTiles = 0;
            foreach (Zone z in inWorld.adjacentAreas)
            {
                numberOfTiles += (z.mapHeight * z.mapWidth);
            }

            //create vertex and index buffers
            //4 vertexes per tile, 6 indexes per tile
            //we are using ushort for memory reasons
            //VertexPositionNormalTexture so we can have a pretty texture
            vb = new VertexBuffer(graphics.GraphicsDevice, typeof(CustomVertex), 4 * numberOfTiles, BufferUsage.WriteOnly);
            ib = new IndexBuffer(graphics.GraphicsDevice, IndexElementSize.SixteenBits, 6 * numberOfTiles, BufferUsage.WriteOnly);

            //create a list of tiles per texture
            //this is used to calculate which offsets to use
            tilesPerTex = new ushort[groundTexture.Count+1];
            for (int i = 0; i < groundTexture.Count+1; i++)
            {
                tilesPerTex[i] = 0;
            }
            //make temporary vertex and index storage
            List<CustomVertex> vertices = new List<CustomVertex>();
            List<ushort> indices = new List<ushort>();

            //loop through every visible tile
            foreach (Zone z in inWorld.adjacentAreas)
            {
                //calculate offsets to use for the vertices
                float xOffset = (float)z.globalX * 1f;
                float yOffset = (float)z.globalY * 1f;

                for (int x = 0; x < z.mapWidth; x++)
                {
                    for (int y = 0; y < z.mapHeight; y++)
                    {
                        float height = z.tile[x, y].Z;
                        //vertexes go TL, TR, BR, BL, clockwise
                        Vector3 TL = new Vector3(xOffset + (float)x * 1f - 0.5f, (float)height/8, yOffset + (float)y * 1f - 0.5f);
                        Vector3 TR = new Vector3(xOffset + (float)x * 1f + 0.5f, (float)height / 8, yOffset + (float)y * 1f - 0.5f);
                        Vector3 BR = new Vector3(xOffset + (float)x * 1f + 0.5f, (float)height / 8, yOffset + (float)y * 1f + 0.5f);
                        Vector3 BL = new Vector3(xOffset + (float)x * 1f - 0.5f, (float)height / 8, yOffset + (float)y * 1f + 0.5f);

                        //4 vertices for 4 corners
                        vertices.Add(new CustomVertex(TL, normal, TLTex, TLTex));
                        vertices.Add(new CustomVertex(TR, normal, TRTex, TRTex));
                        vertices.Add(new CustomVertex(BR, normal, BRTex, BRTex));
                        vertices.Add(new CustomVertex(BL, normal, BLTex, BLTex));
                    }//end y loop
                }//end x loop
            }//end zone loop
            //fill vertex buffer
            vb.SetData(vertices.ToArray());

            //keep track of the texture we are using
            int texture = 0;
            //for each tile type
            foreach (KeyValuePair<String, Texture2D> kvp in groundTexture)
            {
                //keep track of the tile vertexes we are up to
                int count = 3;
                //loop through every visible tile
                foreach (Zone z in inWorld.adjacentAreas)
                {
                    for (int x = 0; x < z.mapWidth; x++)
                    {
                        for (int y = 0; y < z.mapHeight; y++)
                        {
                            //if the current tile has the current tile type
                            if (z.tile[x, y].tileType == kvp.Key)
                            {
                                //6 indices because a quad is 2 triangles
                                indices.Add(Convert.ToUInt16(count - 3));
                                indices.Add(Convert.ToUInt16(count - 1));
                                indices.Add(Convert.ToUInt16(count - 0));
                                indices.Add(Convert.ToUInt16(count - 3));
                                indices.Add(Convert.ToUInt16(count - 2));
                                indices.Add(Convert.ToUInt16(count - 1));

                                //add one to tiles of this texture
                                tilesPerTex[texture] += 1;
                            }
                            //increment count
                            count += 4;
                        }//end y loop
                    }//end x loop
                }//end zone loop
                texture++;
            }//end texture loop
            //for any tiles that have textures that are not in the texture list
            //add a section for 'missing asset' tiles
            {
                //keep track of the tile vertexes we are up to
                int count = 3;
                //loop through every visible tile
                foreach (Zone z in inWorld.adjacentAreas)
                {
                    for (int x = 0; x < z.mapWidth; x++)
                    {
                        for (int y = 0; y < z.mapHeight; y++)
                        {
                            //if the current tile has a missing asset
                            if (!groundTexture.ContainsKey(z.tile[x, y].tileType))
                            {
                                //6 indices because a quad is 2 triangles
                                indices.Add(Convert.ToUInt16(count - 3));
                                indices.Add(Convert.ToUInt16(count - 1));
                                indices.Add(Convert.ToUInt16(count - 0));
                                indices.Add(Convert.ToUInt16(count - 3));
                                indices.Add(Convert.ToUInt16(count - 2));
                                indices.Add(Convert.ToUInt16(count - 1));

                                //add one to missing assets count
                                tilesPerTex[groundTexture.Count] += 1;
                            }
                            //increment count
                            count += 4;
                        }//end y loop
                    }//end x loop
                }//end zone loop
                texture++;
            }

            //set index data
            ib.SetData<ushort>(indices.ToArray());

            //now we have vertices, indexes, and number of tiles to draw
            vertices = null;
            indices = null;
        }

        //draws adjacent ground and the current map given a player      
        public static void DrawAdjacentGround(World inWorld, PC.Player inPlayer)
        {
                float moveRatio = (float)inPlayer.movementIndex / (float)inPlayer.speed;
                Vector3 drawLocation = new Vector3();
                drawLocation.X = inWorld.currentArea.globalX + inPlayer.tileCoords.X + moveRatio * (inPlayer.nextTile.X - inPlayer.tileCoords.X);
                drawLocation.Y = inWorld.currentArea.globalY + inPlayer.tileCoords.Y + moveRatio * (inPlayer.nextTile.Y - inPlayer.tileCoords.Y);
                drawLocation.Z = (float)inPlayer.currentZ + moveRatio * ((float)inWorld.currentArea.tile[inPlayer.nextTile.X, inPlayer.nextTile.Y].Z - (float)inPlayer.currentZ);
                drawLocation.Z = drawLocation.Z / 8;
                DrawAdjacentGround(inWorld, drawLocation);
        }

        //draws the adjacent ground using a vector3 location
        public static void DrawAdjacentGround(World inWorld, Vector3 location)
        {
            effect.CurrentTechnique = effect.Techniques[0];
            graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
            graphics.GraphicsDevice.DepthStencilState = DepthStencilState.Default; //set stencil state
            playerPosition = new Vector3(location.X, location.Z, location.Y);
            cameraPosition = playerPosition + cameraOffset;
            //SamplerState temp = null;

            //create our world view projection matrices
            world = Matrix.Identity;
            view = Matrix.CreateLookAt(cameraPosition, playerPosition, Vector3.Up);
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f),
                aspectRatio, 1.0f, 10000.0f);

            effect.Parameters["World"].SetValue(world);
            effect.Parameters["View"].SetValue(view);
            effect.Parameters["Projection"].SetValue(projection);

            //set our vertex and index buffers
            graphics.GraphicsDevice.SetVertexBuffer(vb);
            graphics.GraphicsDevice.Indices = ib;

            //keep track of number of tiles drawn
            int drawnTiles = 0;
            //for each texture
            //draw each texture
            for (int tex = 0; tex < tilesPerTex.Length - 1; tex++)
            {
                //don't do anything if there are none of that texture
                if (tilesPerTex[tex] != 0)
                {
                    //apply the texture
                    graphics.GraphicsDevice.Textures[0] = groundTexture.Values[tex];
                    //if there is something to draw apply the effect and draw
                    foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                    {
                        pass.Apply();
                        graphics.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, numberOfTiles * 4, drawnTiles * 6, tilesPerTex[tex] * 2);
                    }
                    drawnTiles += tilesPerTex[tex];
                }
            }
            //now draw missing assets
            {
                //don't do anything if there are none of that texture
                if (tilesPerTex[tilesPerTex.Length-1] != 0)
                {
                    //apply the texture
                    graphics.GraphicsDevice.Textures[0] = missingAssetTex;
                    //if there is something to draw apply the effect and draw
                    foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                    {
                        pass.Apply();
                        graphics.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, numberOfTiles * 4, drawnTiles * 6, tilesPerTex[tilesPerTex.Length - 1] * 2);
                    }
                    drawnTiles += tilesPerTex[tilesPerTex.Length - 1];
                }
            }
        }

        //method 2 of drawground
        //draws adjacent ground and the current map
        /*
        public static void drawAdjacentGround(World inWorld, PC.Player inPlayer)
        {
            playerPosition = new Vector3(inPlayer.mapCoords.X * 32, 0, inPlayer.mapCoords.Y * 32);
            cameraPosition = playerPosition + cameraOffset;
            SamplerState temp = null;

            int numberOfTiles = 0;
            foreach (Zone z in inWorld.adjacentAreas)
            {
                numberOfTiles += (z.mapHeight * z.mapWidth);
            }

            //create our world view projection matrices
            Matrix world = Matrix.Identity;
            Matrix view = Matrix.CreateLookAt(cameraPosition, playerPosition, Vector3.Up);
            Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f),
                aspectRatio, 1.0f, 10000.0f);

            //make an effect to use
            //apply the texture and other effects
            BasicEffect effect = new BasicEffect(graphics.GraphicsDevice); //make new graphics device
            effect.TextureEnabled = true;
            //effect.EnableDefaultLighting();
            effect.World = world;
            effect.View = view;
            effect.Projection = projection;

            Vector3 normal = Vector3.Up;
            Vector2 TLTex = new Vector2(0f, 0f);
            Vector2 TRTex = new Vector2(1f, 0f);
            Vector2 BRTex = new Vector2(1f, 1f);
            Vector2 BLTex = new Vector2(0f, 1f);      

            VertexPositionNormalTexture[] vertices = new VertexPositionNormalTexture[4];
            vertices[0] = new VertexPositionNormalTexture(new Vector3(-0.5f, 0, -0.5f), normal, TLTex);
            vertices[1] = new VertexPositionNormalTexture(new Vector3(+0.5f, 0, -0.5f), normal, TRTex);
            vertices[2] = new VertexPositionNormalTexture(new Vector3(+0.5f, 0, +0.5f), normal, BRTex);
            vertices[3] = new VertexPositionNormalTexture(new Vector3(-0.5f, 0, +0.5f), normal, BLTex);
            short[] indices = new short[6];
            indices[0] = 0;
            indices[1] = 2;
            indices[2] = 3;
            indices[3] = 0;
            indices[4] = 1;
            indices[5] = 2;

            foreach( KeyValuePair<String, Texture2D> kvp in groundTexture)
            {
                
                foreach (Zone z in inWorld.adjacentAreas)
                {
                    for (int x = 0; x < z.mapWidth; x++)
                    {
                        for (int y = 0; y < z.mapHeight; y++)
                        {
                            if (z.tile[x, y].tileType == kvp.Key)
                            {
                                effect.World = Matrix.CreateTranslation(new Vector3(z.globalX * 1f + x * 1f, z.tile[x, y].Z * 0.5f, z.globalY * 1f + y * 1f));
                                effect.Texture = kvp.Value;

                                foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                                {
                                    pass.Apply();
                                    graphics.GraphicsDevice.DrawUserIndexedPrimitives<VertexPositionNormalTexture>(PrimitiveType.TriangleList, vertices, 0, 4, indices, 0, 2);
                                }
                            }
                        }//end y loop
                    }//end x loop
                }//endzone loop
            }

        }*/

        /* old
        public static void drawPlayer(PC.Player inPlayer)
        {
            playerPosition = new Vector3(inPlayer.mapCoords.X * 32, 0, inPlayer.mapCoords.Y * 32);
            cameraPosition = playerPosition + cameraOffset;
            SamplerState temp = null;
            //get the model's transforms
            Matrix[] transforms = new Matrix[spriteModel.Bones.Count];
            spriteModel.CopyAbsoluteBoneTransformsTo(transforms);

            RasterizerState state = new RasterizerState();
            state.CullMode = CullMode.None;
            graphics.GraphicsDevice.RasterizerState = state;

            // Draw the model. A model can have multiple meshes, so loop.
            foreach (ModelMesh mesh in spriteModel.Meshes)
            {
                // This is where the mesh orientation is set, as well as our camera and projection.
                foreach (BasicEffect effect in mesh.Effects)
                {
                    temp = graphics.GraphicsDevice.SamplerStates[0];
                    graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;

                    effect.TextureEnabled = true;
                    effect.Texture = inPlayer.textureSheet;

                    effect.EnableDefaultLighting();
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateScale(1.0f) * Matrix.CreateBillboard(playerPosition, cameraPosition, Vector3.Up, null) ;
                    effect.View = Matrix.CreateLookAt(cameraPosition, playerPosition, Vector3.Up);
                    effect.Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f),
                    aspectRatio, 1.0f, 10000.0f);
                }
                    // Draw the mesh, using the effects set above.
                mesh.Draw();
                graphics.GraphicsDevice.SamplerStates[0] = temp;
                            
            }//end model drawing
            state = new RasterizerState();
            state.CullMode = CullMode.CullCounterClockwiseFace;
            graphics.GraphicsDevice.RasterizerState = state;
        }*/

        /// <summary>
        /// Draws the player
        /// </summary>
        /// <param name="spritePosition">position of the sprite to draw on the spitesheet</param>
        /// <param name="spritesheetSize">total size of entire spritesheet</param>
        public static void DrawPlayer(PC.Player inPlayer, World inWorld, Rectangle spritePosition, Rectangle spritesheetSize)
        {
            graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
            effect.CurrentTechnique = effect.Techniques[0];
            graphics.GraphicsDevice.DepthStencilState = DepthStencilState.Default; //set stencil state
            graphics.GraphicsDevice.BlendState = BlendState.AlphaBlend;
            float moveRatio = (float)inPlayer.movementIndex / (float)inPlayer.speed;
            Vector3 drawLocation = new Vector3();
            drawLocation.X = inWorld.currentArea.globalX + (float)inPlayer.tileCoords.X + moveRatio * ((float)inPlayer.nextTile.X - (float)inPlayer.tileCoords.X);
            drawLocation.Y = (float)inPlayer.currentZ + moveRatio * ((float)inWorld.currentArea.tile[inPlayer.nextTile.X, inPlayer.nextTile.Y].Z - (float)inPlayer.currentZ);
            drawLocation.Y = drawLocation.Y / 8;
            drawLocation.Z = inWorld.currentArea.globalY + (float)inPlayer.tileCoords.Y + moveRatio * ((float)inPlayer.nextTile.Y - (float)inPlayer.tileCoords.Y);
            playerPosition = drawLocation;
            cameraPosition = playerPosition + cameraOffset;
            //billboard does weird things with culling

            Vector3 normal = Vector3.Backward;
            Vector2 TLTex = new Vector2((float)(spritePosition.X  ) / (float)spritesheetSize.Width, (float)(spritePosition.Y  ) / (float)spritesheetSize.Height);
            Vector2 TRTex = new Vector2(((float)(spritePosition.X  ) + (float)spritePosition.Width) / (float)spritesheetSize.Width,
                                        (float)(spritePosition.Y  ) / (float)spritesheetSize.Height);
            Vector2 BRTex = new Vector2(((float)(spritePosition.X  ) + (float)spritePosition.Width) / (float)spritesheetSize.Width,
                                        ((float)(spritePosition.Y  ) + (float)spritePosition.Height) / (float)spritesheetSize.Height);
            Vector2 BLTex = new Vector2((float)(spritePosition.X  ) / (float)spritesheetSize.Width,
                                        ((float)(spritePosition.Y  ) + (float)spritePosition.Height) / (float)spritesheetSize.Height);

            //doing them backwards because the billboarding flips them for some reason
            playerVertices[0] = new CustomVertex(new Vector3(+0.8125f, +1.625f, 0), Vector3.Forward, TLTex, TLTex);
            playerVertices[1] = new CustomVertex(new Vector3(-0.8125f, +1.625f, 0), Vector3.Forward, TRTex, TRTex);
            playerVertices[2] = new CustomVertex(new Vector3(-0.8125f, 0, 0), Vector3.Forward, BRTex, BRTex);
            playerVertices[3] = new CustomVertex(new Vector3(+0.8125f, 0, 0), Vector3.Forward, BLTex, BLTex);
            //playerVertices[0] = new CustomVertex(new Vector3(+0.5f, +1f, 0), Vector3.Forward, TLTex, TLTex);
            //playerVertices[1] = new CustomVertex(new Vector3(-0.5f, +1f, 0), Vector3.Forward, TRTex, TRTex);
            //playerVertices[2] = new CustomVertex(new Vector3(-0.5f, 0, 0), Vector3.Forward, BRTex, BRTex);
            //playerVertices[3] = new CustomVertex(new Vector3(+0.5f, 0, 0), Vector3.Forward, BLTex, BLTex);
            playerIndices[0] = 0;
            playerIndices[1] = 2;
            playerIndices[2] = 3;
            playerIndices[3] = 0;
            playerIndices[4] = 1;
            playerIndices[5] = 2;


            //set effect parameters
            effect.Parameters["World"].SetValue(Matrix.CreateBillboard(playerPosition + new Vector3(0, 0, 0.4375f), cameraPosition, Vector3.Up, null)); //the new vector is to make the player appear at the bottom of the tile
            effect.Parameters["View"].SetValue(view);
            effect.Parameters["Projection"].SetValue(projection);
            graphics.GraphicsDevice.Textures[0] = inPlayer.textureSheet;

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphics.GraphicsDevice.DrawUserIndexedPrimitives<CustomVertex>(PrimitiveType.TriangleList, playerVertices, 0, 4, playerIndices, 0, 2);
            }

            
        }


        /// <summary>
        /// Draws all NPCs in the current area
        /// </summary>
        /// <param name="inWorld">the current world</param>
        public static void DrawNPCs(World inWorld)
        {
            if (!drawAdjObjects)
            {
                if (inWorld.currentArea.trainerList == null)
                    return;

                foreach (NPC npc in inWorld.currentArea.trainerList)
                {
                    DrawNPC(npc, inWorld, npc.spritePosition, spriteSheetSize, inWorld.currentArea);
                }
            }
            else
            {
                foreach (Zone z in inWorld.adjacentAreas)
                {
                    if (z == null)
                        break;

                    foreach (NPC npc in z.trainerList)
                    {
                        DrawNPC(npc, inWorld, npc.spritePosition, spriteSheetSize, z);
                    }
                }
            }
        }

        public static void DrawNPCs(List<NPC> npcs, World inWorld)
        {
            if (inWorld != null && npcs != null)
            {
                foreach (NPC n in npcs)
                {
                    DrawNPC(n, inWorld, n.spritePosition, spriteSheetSize, inWorld.currentArea);
                }
            }
        }

        public static void DrawUncontroledPlayer(NPC p, World inWorld)
        {
            if (inWorld != null && p != null)
            {
                DrawNPC(p, inWorld, p.spritePosition, spriteSheetSize, inWorld.currentArea);
            }
        }

        private static void DrawNPC(NPC inNPC, World inWorld, Rectangle spritePosition, Rectangle spritesheetSize, Zone zone)
        {
            effect.CurrentTechnique = effect.Techniques[0];
            graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
            graphics.GraphicsDevice.DepthStencilState = DepthStencilState.Default; //set stencil state
            graphics.GraphicsDevice.BlendState = BlendState.AlphaBlend;
            int Z = zone.tile[inNPC.tileCoords.X, inNPC.tileCoords.Y].Z;
            float moveRatio = (float)inNPC.movementIndex / (float)inNPC.speed;
            Vector3 drawLocation = new Vector3();
            drawLocation.X = zone.globalX + (float)inNPC.tileCoords.X + moveRatio * ((float)inNPC.nextTile.X - (float)inNPC.tileCoords.X);
            drawLocation.Y = (float)inNPC.currentZ + moveRatio * ((float)zone.tile[inNPC.nextTile.X, inNPC.nextTile.Y].Z - (float)inNPC.currentZ);
            drawLocation.Y = drawLocation.Y / 8;
            drawLocation.Z = zone.globalY + (float)inNPC.tileCoords.Y + moveRatio * ((float)inNPC.nextTile.Y - (float)inNPC.tileCoords.Y);
            playerPosition = drawLocation;
            cameraPosition = playerPosition + cameraOffset;
            //billboard does weird things with culling

            Vector3 normal = Vector3.Backward;
            Vector2 TLTex = new Vector2((float)spritePosition.X / (float)spritesheetSize.Width, (float)spritePosition.Y / (float)spritesheetSize.Height);
            Vector2 TRTex = new Vector2(((float)spritePosition.X + (float)spritePosition.Width) / (float)spritesheetSize.Width,
                                        (float)spritePosition.Y / (float)spritesheetSize.Height);
            Vector2 BRTex = new Vector2(((float)spritePosition.X + (float)spritePosition.Width) / (float)spritesheetSize.Width,
                                        ((float)spritePosition.Y + (float)spritePosition.Height) / (float)spritesheetSize.Height);
            Vector2 BLTex = new Vector2((float)spritePosition.X / (float)spritesheetSize.Width,
                                        ((float)spritePosition.Y + (float)spritePosition.Height) / (float)spritesheetSize.Height);

            float z = inWorld.currentArea.tile[inNPC.tileCoords.X, inNPC.tileCoords.Y].Z;
            //doing them backwards because the billboarding flips them for some reason
            playerVertices[0] = new CustomVertex(new Vector3(+0.8125f, +1.625f, 0), Vector3.Forward, TLTex, TLTex);
            playerVertices[1] = new CustomVertex(new Vector3(-0.8125f, +1.625f, 0), Vector3.Forward, TRTex, TRTex);
            playerVertices[2] = new CustomVertex(new Vector3(-0.8125f, 0, 0), Vector3.Forward, BRTex, BRTex);
            playerVertices[3] = new CustomVertex(new Vector3(+0.8125f, 0, 0), Vector3.Forward, BLTex, BLTex);
            //playerVertices[0] = new CustomVertex(new Vector3(+0.5f, +1f, 0), Vector3.Forward, TLTex, TLTex);
            //playerVertices[1] = new CustomVertex(new Vector3(-0.5f, +1f, 0), Vector3.Forward, TRTex, TRTex);
            //playerVertices[2] = new CustomVertex(new Vector3(-0.5f, 0, 0), Vector3.Forward, BRTex, BRTex);
            //playerVertices[3] = new CustomVertex(new Vector3(+0.5f, 0, 0), Vector3.Forward, BLTex, BLTex);
            playerIndices[0] = 0;
            playerIndices[1] = 2;
            playerIndices[2] = 3;
            playerIndices[3] = 0;
            playerIndices[4] = 1;
            playerIndices[5] = 2;


            //set effect parameters
            effect.Parameters["World"].SetValue(Matrix.CreateBillboard(playerPosition + new Vector3(0, 0, 0.4375f), cameraPosition, Vector3.Up, null) * Matrix.CreateTranslation(0, z, 0)); //the new vector is to make the player appear at the bottom of the tile
            effect.Parameters["View"].SetValue(view);
            effect.Parameters["Projection"].SetValue(projection);

            foreach(KeyValuePair<String, Texture2D> kvp in NPCTextures)
            {
                if (kvp.Key == inNPC.spriteSheet)
                {
                    graphics.GraphicsDevice.Textures[0] = kvp.Value;
                    break;
                }
            }

            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphics.GraphicsDevice.DrawUserIndexedPrimitives<CustomVertex>(PrimitiveType.TriangleList, playerVertices, 0, 4, playerIndices, 0, 2);
            }

        }

        public static void UpdateNPCSpritesheets(World inWorld)
        {
            //empty the current bunch of textures
            NPCTextures.Clear();

            if (inWorld.currentArea.trainerList == null)
            {
                return;
            }

            //go through each NPC in the list of adjacent areas
            foreach (Zone z in inWorld.adjacentAreas)
            {
                foreach (NPC npc in z.trainerList)
                {
                    //add the texture to the list if it isn't already there
                    if (!NPCTextures.ContainsKey(npc.spriteSheet))
                    {
                        string rootDir = Directory.GetCurrentDirectory();
                        //TEXTURES GO INTO \Content\Tiles and are loaded automatically, make sure to set "Copy Always"
                        string path = rootDir + "\\Content\\Sprites\\NPCs\\Overworlds\\" + npc.spriteSheet;

                        //SD is using the system drawing namespace
                        //SD.Bitmap image = new SD.Bitmap(path);
                        //SD.Graphics imageGraphics = SD.Graphics.FromImage(image);
                        String[] temp = path.Split('\\');
                        //Texture2D newTex = new Texture2D(graphics.GraphicsDevice, image.Width, image.Height);
                        Texture2D newTex = Texture2D.FromStream(graphics.GraphicsDevice, new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read));

                        //Now we want to apply transparency
                        //first get the raw data from the image
                        Color[] gottenColour = new Color[newTex.Width * newTex.Height];
                        newTex.GetData<Color>(gottenColour);
                        //sample the pixel at 5,5 to get the transparent colour to use
                        Color sample = new Color(gottenColour[newTex.Width * 5 + 5].R,
                                                gottenColour[newTex.Width * 5 + 5].G,
                                                gottenColour[newTex.Width * 5 + 5].B,
                                                gottenColour[newTex.Width * 5 + 5].A);
                        for (int i = 0; i < newTex.Width * newTex.Height - 1; i++)
                        {
                            if (gottenColour[i].R == sample.R &&
                                gottenColour[i].G == sample.G &&
                                gottenColour[i].B == sample.B)
                            {
                                gottenColour[i].R = gottenColour[i].G = gottenColour[i].B = gottenColour[i].A = 0;
                            }
                        }
                        //get rid of black edges around sprite
                        for (int x = 0; x < 3; x++)
                        {
                            for (int y = 0; y < 4; y++)
                            {
                                int i = 0;
                                //top
                                for (i = 0; i < 34; i++)
                                    gottenColour[(y * 5 + y * 32 + 4) * newTex.Width + (x * 5 + x * 32 + 4) + i] = new Color(0f, 0f, 0f, 0f);
                                //bottom
                                for (i = 0; i < 34; i++)
                                    gottenColour[(y * 5 + (y + 1) * 32 + 5) * newTex.Width + (x * 5 + x * 32 + 4) + i] = new Color(0f, 0f, 0f, 0f);
                                //left
                                for (i = 0; i < 34; i++)
                                    gottenColour[(y * 5 + y * 32 + 4 + i) * newTex.Width + (x * 5 + x * 32 + 4)] = new Color(0f, 0f, 0f, 0f);
                                //right
                                for (i = 0; i < 34; i++)
                                    gottenColour[(y * 5 + y * 32 + 4 + i) * newTex.Width + (x * 5 + (x + 1) * 32 + 5)] = new Color(0f, 0f, 0f, 0f);
                            }
                        }
                        newTex.SetData<Color>(gottenColour);

                        NPCTextures.Add(npc.spriteSheet, newTex);
                    }
                }
            }
        }

        /// <summary>
        /// Draws all the scenery in the given zone
        /// </summary>
        /// <param name="inZone"></param>
        public static void DrawScenery(World inWorld)
        {
            //Debug();

            //draw all the scenery
            if (drawAdjObjects)
            {
                foreach (Zone z in inWorld.adjacentAreas)
                {
                    foreach (Scenery s in z.scenery)
                    {
                        DrawScenery(s, z.globalX, z.globalY, z.tile[s.position.X, s.position.Y].Z);
                    }
                }
            }
            else
            {
                foreach (Scenery s in inWorld.currentArea.scenery)
                {
                    DrawScenery(s, inWorld.currentArea.globalX, inWorld.currentArea.globalY, inWorld.currentArea.tile[s.position.X, s.position.Y].Z);
                }
            }
        }

        private static void Debug()
        {
            Model mod = contentManager.Load<Model>("Models\\" + "booox");

            // Copy any parent transforms.
            Matrix[] transforms = new Matrix[mod.Bones.Count];
            mod.CopyAbsoluteBoneTransformsTo(transforms);
            foreach (ModelMesh mesh in mod.Meshes)
            {
                effect.Parameters["World"].SetValue(transforms[mesh.ParentBone.Index]);
                effect.Parameters["View"].SetValue(view);
                effect.Parameters["Projection"].SetValue(projection);

                // Draw the mesh, using the effects set above.
                foreach(ModelMeshPart part in mesh.MeshParts)
                {
                    if (part.VertexBuffer.VertexDeclaration.VertexStride == 40)
                        effect.CurrentTechnique = effect.Techniques[0];
                    else
                        effect.CurrentTechnique = effect.Techniques[1];

                    graphics.GraphicsDevice.Textures[0] = part.Effect.Parameters["Texture"].GetValueTexture2D();
                    foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                    {
                        pass.Apply();
                        graphics.GraphicsDevice.SetVertexBuffer(part.VertexBuffer);
                        graphics.GraphicsDevice.Indices = part.IndexBuffer;
                        graphics.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, part.VertexOffset, 0, part.NumVertices, part.StartIndex, part.PrimitiveCount);
                    }
                }
            }
        }

        public static void DrawScenery(Scenery s, int x, int y, short z)
        {

            if (s != null)
            {
                if (File.Exists("Content\\Models\\" + s.modelName + ".xnb"))
                {
                        Model mod = contentManager.Load<Model>("Models\\" + s.modelName);

                    graphics.GraphicsDevice.SamplerStates[0] = SamplerState.PointClamp;
                    // Copy any parent transforms.
                    Matrix[] transforms = new Matrix[mod.Bones.Count];
                    mod.CopyAbsoluteBoneTransformsTo(transforms);
                    foreach (ModelMesh mesh in mod.Meshes)
                    {

                        effect.Parameters["World"].SetValue(transforms[mesh.ParentBone.Index] * s.rotation * s.scale * s.translation * Matrix.CreateTranslation(new Vector3(x + s.position.X - 0.5f, (float)z/8f, y + s.position.Y - 0.5f)));
                        effect.Parameters["View"].SetValue(view);
                        effect.Parameters["Projection"].SetValue(projection);

                        // Draw the mesh, using the effects set above.
                        foreach (ModelMeshPart part in mesh.MeshParts)
                        {
                            graphics.GraphicsDevice.Textures[0] = part.Effect.Parameters["Texture"].GetValueTexture2D();
                            if (part.VertexBuffer.VertexDeclaration.VertexStride == 40)
                                effect.CurrentTechnique = effect.Techniques[0];
                            else
                                effect.CurrentTechnique = effect.Techniques[1];
                            foreach (EffectPass pass in effect.CurrentTechnique.Passes)
                            {
                                pass.Apply();
                                graphics.GraphicsDevice.SetVertexBuffer(part.VertexBuffer);
                                graphics.GraphicsDevice.Indices = part.IndexBuffer;
                                graphics.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, part.VertexOffset, 0, part.NumVertices, part.StartIndex, part.PrimitiveCount);
                            }
                        }
                    }
                }
            }
        }
          
    }

    struct CustomVertex : IVertexType
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector2 UV;
        public Vector2 UV2;

        public static readonly VertexDeclaration _dec1 = new VertexDeclaration(
            new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
            new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0),
            new VertexElement(24, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 0),
            new VertexElement(32, VertexElementFormat.Vector2, VertexElementUsage.TextureCoordinate, 1));

        public CustomVertex(Vector3 position, Vector3 normal, Vector2 uv, Vector2 uv2)
        {
            Position = position;
            Normal = normal;
            UV = uv;
            UV2 = uv2;
        }

        public VertexDeclaration VertexDeclaration
        {
            get { return _dec1; }
        }
    }

}
