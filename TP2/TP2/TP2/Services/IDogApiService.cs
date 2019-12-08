using System.Collections.Generic;
using TP2.Models;

namespace TP2.Services
{
    public interface IDogApiService
    {
        RootObject GetDogBreeds();

        RandomImage GetRandomImageURL(string Url);

    }
}