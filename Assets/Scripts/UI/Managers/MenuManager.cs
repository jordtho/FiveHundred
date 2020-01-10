﻿using Assets.Scripts.UI.Behaviors;
using Assets.Scripts.UI.Repositories;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Assets.Scripts.UI.Managers
{
    public class MenuManager
    {
        private readonly MenuRepository _menuRepository;

        public MenuManager(MenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
            _menuRepository.Initialize();
        }

        public Button GetButton(string label, IEnumerable<Action<string>> actions)
        {
            var button = _menuRepository.CreateButton(label);
            foreach (var action in actions) { button.onClick.AddListener(() => action(label)); }
            return button;
        }

        public Button GetButton(string label, Action<string> action) => GetButton(label, new[] { action });

        public Button GetButton(string label) => GetButton(label, (string _) => { });

        public List<Button> GetButtons(IEnumerable<string> labels, IEnumerable<Action<string>> actions)
        {
            var buttons = new List<Button>();
            foreach (var label in labels) { buttons.Add(GetButton(label, actions)); }
            return buttons;
        }

        public Button GetButton<T>(T value, IEnumerable<Action<T>> actions) where T : Enum
        {
            var button = _menuRepository.CreateButton(value.ToString());
            foreach (var action in actions) { button.onClick.AddListener(() => action(value)); }
            return button;
        }

        public Button GetButton<T>(T value, Action<T> action) where T : Enum => GetButton(value, new[] { action });

        public List<Button> GetButtons<T>(IEnumerable<T> values, IEnumerable<Action<T>> actions) where T : Enum
        {
            var buttons = new List<Button>();
            foreach (var value in values) { buttons.Add(GetButton(value, actions)); }
            return buttons;
        }

        public List<Button> GetButtons<T>(IEnumerable<T> values, Action<T> action) where T : Enum => GetButtons(values, new[] { action });

        public Menu GetMenu(string title, IEnumerable<Button> buttons) => _menuRepository.CreateMenu(title, buttons);
    }
}
