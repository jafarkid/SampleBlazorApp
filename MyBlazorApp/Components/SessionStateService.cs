using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyBlazorApp.Components
{
    public class SessionStateService
    {
        public string Token { get; set; }
    }
}