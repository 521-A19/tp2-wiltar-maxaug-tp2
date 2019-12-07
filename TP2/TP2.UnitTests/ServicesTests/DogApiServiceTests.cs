using TP2.Services;
using System;
using Xunit;
using System.Threading.Tasks;
using FluentAssertions;

namespace TP2.UnitTests
{
    public class DogApiServiceTests
    {
        private readonly DogApiService _apiService;

        public DogApiServiceTests()
        {
            _apiService = new DogApiService();
        }

        [Fact]
        public void GetDogBreeds_WhenJsonDeserializeIsCall_ShouldReturnAVariableNotNull()
        {
            var breeds = _apiService.GetDogBreeds();

            breeds.Should().NotBeNull();


        }

        [Fact]
        public void GetDogBreeds_WhenDeserializeIsMade_ShouldReturnAStatusSuccess()
        {
            var breeds = _apiService.GetDogBreeds();

            breeds.status.Should().BeEquivalentTo("success");


        }

        [Fact]
        public void GetRandomImageURL_WhenJsonDeserializeIsCall_ShouldReturnAVariableNotNull()
        {
            const string GOOD_URL = "https://dog.ceo/api/breed/african/images/random";

            var image = _apiService.GetRandomImageURL(GOOD_URL);

            image.Should().NotBeNull();
        }


        [Fact]
        public void GetRandomImageURL_WhenDeserializeIsMade_ShouldReturnAStatusSuccess()
        {
            const string GOOD_URL = "https://dog.ceo/api/breed/african/images/random";

            var image = _apiService.GetRandomImageURL(GOOD_URL);

            image.status.Should().BeEquivalentTo("success");
        }
    }
}
