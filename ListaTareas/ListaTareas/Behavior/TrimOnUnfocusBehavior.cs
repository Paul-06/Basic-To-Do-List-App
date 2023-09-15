using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ListaTareas.Behavior
{
    // Definimos una clase que hereda de Behavior<Entry>
    public class TrimOnUnfocusBehavior : Behavior<Entry>
    {
        // Este método se llama cuando el comportamiento se adjunta a un Entry
        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            // Agregamos un manejador de eventos para el evento Unfocused del Entry
            bindable.Unfocused += Bindable_Unfocused;
        }

        // Este método se llama cuando el comportamiento se desvincula de un Entry
        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            // Eliminamos el manejador de eventos para el evento Unfocused del Entry
            bindable.Unfocused -= Bindable_Unfocused;
        }

        // Este es el manejador de eventos para el evento Unfocused
        private void Bindable_Unfocused(object sender, FocusEventArgs e)
        {
            // Obtenemos el Entry que perdió el foco
            var entry = sender as Entry;
            if (entry != null)
            {
                // Quitamos los espacios en blanco al principio y al final del texto del Entry
                entry.Text = entry.Text.Trim();
            }
        }
    }

}
