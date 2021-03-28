/*
 * Swagger Petstore
 *
 * This is a sample server Petstore server.  You can find out more about Swagger at [http://swagger.io](http://swagger.io) or on [irc.freenode.net, #swagger](http://swagger.io/irc/).  For this sample, you can use the api key `special-key` to test the authorization filters.
 *
 * The version of the OpenAPI document: 1.0.5
 * Contact: apiteam@swagger.io
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using OpenapiGenerator.Server.Attributes;
using OpenapiGenerator.Server.Models;

namespace OpenapiGenerator.Server.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class PetApiController : ControllerBase
    { 
        /// <summary>
        /// Add a new pet to the store
        /// </summary>
        /// <param name="body">Pet object that needs to be added to the store</param>
        /// <response code="405">Invalid input</response>
        [HttpPost]
        [Route("/pet")]
        [ValidateModelState]
        [SwaggerOperation("AddPet")]
        public virtual IActionResult AddPet([FromBody]Pet body)
        { 

            //TODO: Uncomment the next line to return response 405 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(405);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes a pet
        /// </summary>
        /// <param name="petId">Pet id to delete</param>
        /// <param name="apiKey"></param>
        /// <response code="400">Invalid ID supplied</response>
        /// <response code="404">Pet not found</response>
        [HttpDelete]
        [Route("/pet/{petId}")]
        [ValidateModelState]
        [SwaggerOperation("DeletePet")]
        public virtual IActionResult DeletePet([FromRoute (Name = "petId")][Required]long petId, [FromHeader]string apiKey)
        { 

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400);
            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Finds Pets by status
        /// </summary>
        /// <remarks>Multiple status values can be provided with comma separated strings</remarks>
        /// <param name="status">Status values that need to be considered for filter</param>
        /// <response code="200">successful operation</response>
        /// <response code="400">Invalid status value</response>
        [HttpGet]
        [Route("/pet/findByStatus")]
        [ValidateModelState]
        [SwaggerOperation("FindPetsByStatus")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<Pet>), description: "successful operation")]
        public virtual IActionResult FindPetsByStatus([FromQuery (Name = "status")][Required()]List<string> status)
        { 

            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(List<Pet>));
            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400);
            string exampleJson = null;
            exampleJson = "{\r\n  \"photoUrls\" : [ \"photoUrls\", \"photoUrls\" ],\r\n  \"name\" : \"doggie\",\r\n  \"id\" : 0,\r\n  \"category\" : {\r\n    \"name\" : \"name\",\r\n    \"id\" : 6\r\n  },\r\n  \"tags\" : [ {\r\n    \"name\" : \"name\",\r\n    \"id\" : 1\r\n  }, {\r\n    \"name\" : \"name\",\r\n    \"id\" : 1\r\n  } ],\r\n  \"status\" : \"available\"\r\n}";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<List<Pet>>(exampleJson)
            : default(List<Pet>);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Finds Pets by tags
        /// </summary>
        /// <remarks>Multiple tags can be provided with comma separated strings. Use tag1, tag2, tag3 for testing.</remarks>
        /// <param name="tags">Tags to filter by</param>
        /// <response code="200">successful operation</response>
        /// <response code="400">Invalid tag value</response>
        [HttpGet]
        [Route("/pet/findByTags")]
        [ValidateModelState]
        [SwaggerOperation("FindPetsByTags")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<Pet>), description: "successful operation")]
        public virtual IActionResult FindPetsByTags([FromQuery (Name = "tags")][Required()]List<string> tags)
        { 

            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(List<Pet>));
            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400);
            string exampleJson = null;
            exampleJson = "{\r\n  \"photoUrls\" : [ \"photoUrls\", \"photoUrls\" ],\r\n  \"name\" : \"doggie\",\r\n  \"id\" : 0,\r\n  \"category\" : {\r\n    \"name\" : \"name\",\r\n    \"id\" : 6\r\n  },\r\n  \"tags\" : [ {\r\n    \"name\" : \"name\",\r\n    \"id\" : 1\r\n  }, {\r\n    \"name\" : \"name\",\r\n    \"id\" : 1\r\n  } ],\r\n  \"status\" : \"available\"\r\n}";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<List<Pet>>(exampleJson)
            : default(List<Pet>);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Find pet by ID
        /// </summary>
        /// <remarks>Returns a single pet</remarks>
        /// <param name="petId">ID of pet to return</param>
        /// <response code="200">successful operation</response>
        /// <response code="400">Invalid ID supplied</response>
        /// <response code="404">Pet not found</response>
        [HttpGet]
        [Route("/pet/{petId}")]
        [ValidateModelState]
        [SwaggerOperation("GetPetById")]
        [SwaggerResponse(statusCode: 200, type: typeof(Pet), description: "successful operation")]
        public virtual IActionResult GetPetById([FromRoute (Name = "petId")][Required]long petId)
        { 

            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(Pet));
            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400);
            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);
            string exampleJson = null;
            exampleJson = "{\r\n  \"photoUrls\" : [ \"photoUrls\", \"photoUrls\" ],\r\n  \"name\" : \"doggie\",\r\n  \"id\" : 0,\r\n  \"category\" : {\r\n    \"name\" : \"name\",\r\n    \"id\" : 6\r\n  },\r\n  \"tags\" : [ {\r\n    \"name\" : \"name\",\r\n    \"id\" : 1\r\n  }, {\r\n    \"name\" : \"name\",\r\n    \"id\" : 1\r\n  } ],\r\n  \"status\" : \"available\"\r\n}";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<Pet>(exampleJson)
            : default(Pet);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Update an existing pet
        /// </summary>
        /// <param name="body">Pet object that needs to be added to the store</param>
        /// <response code="400">Invalid ID supplied</response>
        /// <response code="404">Pet not found</response>
        /// <response code="405">Validation exception</response>
        [HttpPut]
        [Route("/pet")]
        [ValidateModelState]
        [SwaggerOperation("UpdatePet")]
        public virtual IActionResult UpdatePet([FromBody]Pet body)
        { 

            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400);
            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);
            //TODO: Uncomment the next line to return response 405 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(405);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates a pet in the store with form data
        /// </summary>
        /// <param name="petId">ID of pet that needs to be updated</param>
        /// <param name="name">Updated name of the pet</param>
        /// <param name="status">Updated status of the pet</param>
        /// <response code="405">Invalid input</response>
        [HttpPost]
        [Route("/pet/{petId}")]
        [ValidateModelState]
        [SwaggerOperation("UpdatePetWithForm")]
        public virtual IActionResult UpdatePetWithForm([FromRoute (Name = "petId")][Required]long petId, [FromForm (Name = "name")]string name, [FromForm (Name = "status")]string status)
        { 

            //TODO: Uncomment the next line to return response 405 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(405);

            throw new NotImplementedException();
        }

        /// <summary>
        /// uploads an image
        /// </summary>
        /// <param name="petId">ID of pet to update</param>
        /// <param name="additionalMetadata">Additional data to pass to server</param>
        /// <param name="file">file to upload</param>
        /// <response code="200">successful operation</response>
        [HttpPost]
        [Route("/pet/{petId}/uploadImage")]
        [ValidateModelState]
        [SwaggerOperation("UploadFile")]
        [SwaggerResponse(statusCode: 200, type: typeof(ApiResponse), description: "successful operation")]
        public virtual IActionResult UploadFile([FromRoute (Name = "petId")][Required]long petId, [FromForm (Name = "additionalMetadata")]string additionalMetadata, [FromForm (Name = "file")]System.IO.Stream file)
        { 

            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(ApiResponse));
            string exampleJson = null;
            exampleJson = "{\r\n  \"code\" : 0,\r\n  \"type\" : \"type\",\r\n  \"message\" : \"message\"\r\n}";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<ApiResponse>(exampleJson)
            : default(ApiResponse);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }
    }
}
