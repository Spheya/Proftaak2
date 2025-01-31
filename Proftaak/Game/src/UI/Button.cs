﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Engine.Rendering;
using Game.Engine.Maths;
using Game.Engine.Input;
using OpenTK;
using OpenTK.Input;
using Game;
using Game.Engine;
using Game.GameStates;

namespace Game.UI
{
    class Button
    {
        public event EventHandler OnClick;
        public void AddToRenderer(SpriteRenderer renderer)
        {
            renderer.Add(_sprite);
        }
        public void RemoveToRenderer(SpriteRenderer renderer)
        {
            renderer.Remove(_sprite);
        }
        int i = 0;
        public void Update(GameWindow window)
        {
            //Console.WriteLine("updated");
            MouseInput.Update();
            Vector2 MousePos = MouseInput.GetMousePos();
            ButtonState MouseLeft = MouseInput.GetMouseLeftButton();
            if (MousePos.X > GetPosition().X + window.Width / 2f - GetSize().X / 2f &&
                    MousePos.X < GetPosition().X  + window.Width / 2f + GetSize().X / 2f &&
                    MousePos.Y < -GetPosition().Y + window.Height / 2f + GetSize().Y / 2f &&
                    MousePos.Y > -GetPosition().Y + window.Height / 2f - GetSize().Y / 2f)
            {
                _sprite.Colour = new Colour(0.5f, 0.5f, 0.5f);

                if (MouseLeft == ButtonState.Pressed && i==0)
                {
                    i = 1;
                    OnClick?.Invoke(this, EventArgs.Empty);
                }
                if (MouseLeft == ButtonState.Released)
                {
                    i = 0;
                }
            }
            else
            {

                _sprite.Colour = new Colour(1.0f, 1.0f, 1.0f);
            }
        }

        Sprite _sprite;
        public Button(Texture texture, Transform transformation)
        {
            _sprite = new Sprite(texture, transformation);
        }
        public Button(Texture texture, Transform transformation, Colour color)
        {
            _sprite = new Sprite(texture, transformation, color);
        }
        public Vector2 GetPosition()
        {
            return new Vector2(_sprite.Transform.Position.X, _sprite.Transform.Position.Y);
        }
        public Vector2 GetSize()
        {
            return new Vector2(_sprite.Transform.Scale.X, _sprite.Transform.Scale.Y);
        }
    }
}
