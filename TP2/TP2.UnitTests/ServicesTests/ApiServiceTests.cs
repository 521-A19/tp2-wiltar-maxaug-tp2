using TP2.Services;
using System;
using Xunit;
using System.Threading.Tasks;
using FluentAssertions;

namespace TP2.UnitTests
{
    public class ApiServiceTests
    {
        private readonly ApiService _apiService;

        public ApiServiceTests()
        {
            _apiService = new ApiService();
        }

        [Fact]
        public void GetBreeds_ShouldReturnAListOfBreeds()
        {
            //barre de chargement
            _apiService.GetBreeds().Should().NotBeEmpty();
        }
    }
}
