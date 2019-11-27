﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Engine.Rendering;
using Game.Engine.Shaders;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using VoxelData;

namespace Game.GameStates
{
    sealed class GameState : ApplicationState
    {

        private Renderer _renderer;

        public override void OnCreate()
        {
            try
            {
                Shader vertexShader = new Shader(ShaderType.VertexShader, 
                    ShaderPreprocessor.Execute(null, File.ReadAllLines(@"res\vertex.glsl"), @"res\"));
                Shader fragmentShader = new Shader(ShaderType.FragmentShader, 
                    ShaderPreprocessor.Execute(null, File.ReadAllLines(@"res\fragment.glsl"), @"res\"));

                _renderer = new Renderer(new ShaderProgram(new[] { vertexShader, fragmentShader }));
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

            Console.WriteLine("Shader compiled <o/"); //epic it work

            VoxelModel model = _renderer.CreateModel(32, 32, 32);
            for (int x = 0; x < 32; x++)
            for (int y = 0; y < 32; y++)
            for (int z = 0; z < 32; z++)
                model[x,y,z] = new Voxel((ushort) ((x + y + z)&1));

            Console.WriteLine("Epic");
        }

        public override void OnUpdate(float deltatime)
        {
            //Random rand = new Random();
            //_model[rand.Next(_model.Width), rand.Next(_model.Height), rand.Next(_model.Depth)] = new Voxel(1);
        }

        public override void OnFixedUpdate(float deltatime)
        {
        }

        public override void OnDraw(float deltatime)
        {
           _renderer.Draw(window);
        }

        public override void OnDestroy()
        {
        }
    }
}
