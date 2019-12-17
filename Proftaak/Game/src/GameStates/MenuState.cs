﻿using System;
using Game.Engine.Rendering;
using Game.Engine.Shaders;
using Game.UI;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using Game.Engine.Maths;
using Game.Engine.Input;
using OpenTK.Input;
using System.Collections.Generic;
using Game.Engine;
using Game.GameStates;

namespace Game.GameStates
{
    class MenuState : ApplicationState
    {
        //public void ButtonAdder()
        //{
        //    Transform _transform2 = new Transform(vector1, Vector3.Zero, new Vector3(window.Width/2, window.Height/2, 30));
        //    _sprite2 = new Sprite(texture1);
        //    _sprite2.Colour = color1;
        //    _sprite2.Transform = _transform2;
        //    _renderer.Add(_sprite2);
        //}
        SpriteRenderer _renderer;
        Sprite _background;
        Button _playbutton;
        Button _playbutton2;
        Button _playbutton3;
        Button _playbutton4;
        List<Button> buttons = new List<Button>();
        public override void OnCreate()
        {
            try
            {
                Shader spriteVertexShader = new Shader(ShaderType.VertexShader, ShaderPreprocessor.Execute(@"res\shaders\ui\vertex.glsl"));
                Shader spriteFragmentShader = new Shader(ShaderType.FragmentShader, ShaderPreprocessor.Execute(@"res\shaders\ui\fragment.glsl"));
                _renderer = new SpriteRenderer(new ShaderProgram(new[] { spriteVertexShader, spriteFragmentShader }));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

            Transform transform1 = new Transform(new Vector3(0, 0, 0), Vector3.Zero, new Vector3(window.Width, window.Height, 30));
            Texture texture1 = new Texture("res\\textures\\mini_yoda.png");
            Colour color1 = new Colour(1, 1, 1);
            Colour color2 = new Colour(0, 0, 1, .9f);
            Colour color3 = new Colour(1, 0, 0, .9f);
            _background = new Sprite(texture1, transform1);
            _background.Colour = color1;
            _renderer.Add(_background);
            int size = 200;
            _playbutton = new Button(texture1, new Transform(new Vector3(-window.Width / 8, -window.Height / 8, 0), Vector3.Zero, new Vector3(size, size, 0)), color2);
            _playbutton2 = new Button(texture1, new Transform(new Vector3(window.Width / 8, -window.Height / 8, 0), Vector3.Zero, new Vector3(size, size, 0)), color2);
            _playbutton3 = new Button(texture1, new Transform(new Vector3(-window.Width / 8, window.Height / 8, 0), Vector3.Zero, new Vector3(size, size, 0)), color2);
            _playbutton4 = new Button(texture1, new Transform(new Vector3(window.Width / 8, window.Height / 8, 0), Vector3.Zero, new Vector3(size, size, 0)), color3);
            buttons.Add(_playbutton);
            buttons.Add(_playbutton2);
            buttons.Add(_playbutton3);
            buttons.Add(_playbutton4);
            Console.WriteLine("Button List: ");
            foreach (Button button in buttons)
            {
                //Console.WriteLine(button);
                button.AddToRenderer(_renderer);
            }
            //ButtonAdder();
            _playbutton.OnClick += _playbutton_OnClick;
            _playbutton2.OnClick += _playbutton2_OnClick;
            _playbutton3.OnClick += _playbutton3_OnClick;
            _playbutton4.OnClick += _playbutton4_OnClick;
        }
        private void _playbutton_OnClick(object sender, EventArgs e)
        {
            //Console.WriteLine("2");
        }
        private void _playbutton2_OnClick(object sender, EventArgs e)
        {
            Console.WriteLine("3");
        }
        private void _playbutton3_OnClick(object sender, EventArgs e)
        {
            Console.WriteLine("1");
        }

        private void _playbutton4_OnClick(object sender, EventArgs e)
        {
            new Window(new GameState()).Run();
        }

        public override void OnDestroy()
        {

        }

        public override void OnDraw(float deltatime)
        {
            _renderer.Draw(window);
        }

        public override void OnFixedUpdate(float deltatime)
        {

        }

        public override void OnUpdate(float deltatime)
        {
            ButtonState MouseLeft = MouseInput.GetMouseLeftButton();
            foreach (Button button in buttons)
            {
                button.Update(window);
            }
            //_playbutton.Update();
            //MouseInput.Update();
            //Vector2 MousePos = MouseInput.GetMousePos();
            //ButtonState MouseLeft = MouseInput.GetMouseLeftButton();
            //Console.WriteLine(MousePos.X);
            //kijkt of het tussen de x-waardes zit (buitendste deel)
            //for (int i = 0; i < buttons.Count; i++)
            //{
            //    Button a = buttons[i];
            //    if (MousePos.X > a.GetPosition().X / 2f + window.Width / 2 - a.GetSize().X / 2f &&
            //        MousePos.X < a.GetPosition().X / 2f + window.Width / 2 + a.GetSize().X / 2f &&
            //        MousePos.Y < -a.GetPosition().Y / 2f + window.Height / 2 + a.GetSize().Y / 2f &&
            //        MousePos.Y > -a.GetPosition().Y / 2f + window.Height / 2 - a.GetSize().Y / 2f)
            //    {
            //        //Console.WriteLine("1");
            //        //Console.WriteLine("2");
            //        //Console.WriteLine(a);
            //        if (a == _playbutton2)
            //        {
            //            if (MouseLeft == ButtonState.Pressed)
            //            {
            //                //Console.WriteLine('1');
            //                //Console.WriteLine('2');
            //new Window(new GameState()).Run();
            //            }
            //        }
            //    }
            //else
            //{
            //    Console.WriteLine("1");
            //    Console.WriteLine("2");
            //}
        }
        }
    }