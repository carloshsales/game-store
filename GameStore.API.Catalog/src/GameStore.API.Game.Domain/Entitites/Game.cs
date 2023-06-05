﻿using GameStore.API.Game.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.API.Game.Domain.Entitites
{
    public class Game
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public string Image { get; private set; }
        public bool IsActive { get; private set; } 
        public DateTime CreatedAt { get; private set; } 

        public Game(string name, string description, decimal price, string image, bool isActive = true)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            Image = image;
            IsActive = isActive;
            CreatedAt = DateTime.Now;

            Validate();
        }

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name)) 
                throw new EntityValidationException($"{nameof(Name)} shold not be empty or null");
            if (Description == null)
                throw new EntityValidationException($"{nameof(Description)} shold not be null");
        }
    }
}