﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsharpVoxReader;
using Game.Engine.Input;
using Game.Engine.Maths;
using Game.Engine.Rendering;
using Game.Engine.Shaders;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using VoxelData;
using VoxLoader;

namespace Game.GameStates
{
    sealed class GameState : ApplicationState
    {

        private readonly FreeCamera _camera = new FreeCamera(new Vector3(0.0f, 0.0f, -32.0f), new Vector3(0.0f, 0.0f, 0.0f));

        private Renderer _renderer;

        private VoxelModel _model;
        private VoxelModel _model2;
        public override void OnCreate()
        {
            try
            {
                Shader vertexShader = new Shader(ShaderType.VertexShader,
                    ShaderPreprocessor.Execute(null, File.ReadAllLines(@"res\shaders\vertex.glsl"), @"res\shaders\"));
                Shader fragmentShader = new Shader(ShaderType.FragmentShader,
                    ShaderPreprocessor.Execute(null, File.ReadAllLines(@"res\shaders\fragment.glsl"), @"res\shaders\"));

                _renderer = new Renderer(new ShaderProgram(new[] { vertexShader, fragmentShader }));
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

            Console.WriteLine("Shader compiled <o/"); //epic it work

            _model = _renderer.CreateModel(32, 32, 32,
                new Transform(new Vector3(24.0f, 0.0f, 0.0f), Vector3.Zero, new Vector3(0.5f)));

            for (int x = 0; x < 32; x++)
            for (int y = 0; y < 32; y++)
            for (int z = 0; z < 32; z++)
                _model[x, y, z] = (byte)((x+y+z)&1);//new Voxel((ushort) ((x + y + z) & 1));


            _model2 = _renderer.CreateModel(72, 126, 72,
                new Transform(new Vector3(-24.0f, 0.0f, 0.0f), Vector3.Zero, new Vector3(0.5f)));

            MyVoxLoader CastleVox = new MyVoxLoader();
            VoxReader r = new VoxReader(@"res\maps\monu10.vox", CastleVox);
            r.Read();

            //for (int x = -16; x < 16; x++)
            //for (int y = -16; y < 16; y++)
            //for (int z = -16; z < 16; z++)
            //    _model2[x + 16, y + 16, z + 16] = (x * x + y * y + z * z < 16 * 16) ? (byte)1 : (byte)0;
            for (int x = 0; x < 72; x++)
            for (int y = 0; y < 126; y++)
            for (int z = 0; z < 72; z++)
                _model2[x,y,z] = CastleVox._data[x,y,z];

            Console.WriteLine("Epic");


            int s = 512;
            VoxelModel model3 = _renderer.CreateModel(s,1,s,
                new Transform(new Vector3(0.0f, -24.0f, 0.0f), Vector3.Zero, new Vector3(1.0f)));

            for (int x = 0; x < s; x++)
            for (int y = 0; y < 1; y++)
            for (int z = 0; z < s; z++)
                model3[x,y,z] = (byte)1;

            Console.WriteLine("Epic");
        }

        public override void OnUpdate(float deltatime)
        {
            window.CursorVisible = !window.Focused;
            if (!window.CursorVisible)
                Mouse.SetPosition(window.X + window.Width * 0.5, window.Y + window.Height * 0.5);

            _model.Transform.Rotation += new Vector3(deltatime, deltatime, deltatime);
            _model2.Transform.Rotation -= new Vector3(deltatime, deltatime, deltatime);

            //Console.WriteLine(_model.Transform.Rotation);

            KeyboardInput.Update();
            MouseInput.Update();
            //Random rand = new Random();
            //_model[rand.Next(_model.Width), rand.Next(_model.Height), rand.Next(_model.Depth)] = new Voxel(1);
            _camera.Update(deltatime);

            //Console.WriteLine(_model.Transform.CalculateInverseMatrix().Column0);
        }

        public override void OnFixedUpdate(float deltatime)
        {
        }

        public override void OnDraw(float deltatime)
        {
           _renderer.Draw(_camera, window);
        }

        public override void OnDestroy()
        {
        }
    }
}
