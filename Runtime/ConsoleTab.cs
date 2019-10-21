using System.Collections.Generic;

namespace TwistedArk.DevelopmentConsole.Runtime
{
    public sealed class ConsoleTab
    {
        private Dictionary<string, GuiElementBase> namedElements = new Dictionary<string, GuiElementBase> ();

        private List<GuiElementBase> elements;

        public string Name { get; private set; }

        public int ElementCount => elements.Count;

        public ConsoleTab (string name, params GuiElementBase[] elements)
        {
            Name = name;
            this.elements = new List<GuiElementBase> (elements);
        }

        public GuiElementBase GetGuiElement (int index)
        {
            return elements[index];
        }

        public ConsoleTab Add (GuiElementBase element)
        {
            elements.Add (element);
            namedElements.Add (element.Label, element);
            return this;
        }

        public ConsoleTab AddRange (IEnumerable<GuiElementBase> elements)
        {
            this.elements.AddRange (elements);
            
            foreach (var element in elements)
            {
                namedElements.Add (element.Label, element);
            }
            
            return this;
        }

        public void RemoveElement (GuiElementBase element)
        {
            namedElements.Remove (element.Label);
            elements.Remove (element);
        }
        
        public bool TryGetNamedElement<T> (string name, out T element) where T : GuiElementBase
        {
            element = null;
            return namedElements.TryGetValue (name, out var temp) && (element = temp as T) != null;
        }
        
    }

}