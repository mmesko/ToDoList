using AutoMapper;
using Todo.Common;
using Todo.Model.Common;
using Todo.Service.Common;
using Todo.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Todo.Mvc.Controllers
{
    [RoutePrefix("api/todo")]
    public class TodoController : ApiController
    {
        private readonly ITodoService service;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service">ITodoService</param>
        public TodoController(ITodoService service)
        {
            this.service = service;
        }

        #region Methods

        /// <summary>
        /// Get by id
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>Resposne with todo/task</returns>
        [Route("getById/{id}")]
        [HttpGet]
        public async Task<HttpResponseMessage> Get(Guid id)
        {
            try
            {
                TodoModel result = Mapper.Map<TodoModel>(await service.GetAsync(id));

                if (result == null)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Error while getting task.");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// Get todos/tasks
        /// </summary>
        /// <param name="pageNumber">Pagenumber</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Collection of tasks/todos</returns>
        [Route("{pageNumber}/{pageSize}")]
        [HttpGet]
        public async Task<HttpResponseMessage> Get(int pageNumber, int pageSize)
        {
            try
            {
                if (pageNumber < 1 || pageSize < 1)
                {
                    pageSize = 1;
                    pageNumber = 1;
                }

                GenericPaging filter = new GenericPaging(pageNumber, pageSize);

                ICollection<TodoModel> result = Mapper.Map<ICollection<TodoModel>>(await service.GetRangeAsync(filter));

                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        /// <summary>
        /// Get todos belongs to one user
        /// </summary>
        /// <param name="pageNumber">Pagenumber</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Collection of todos/tasks</returns>
        [Route("GetByName/{search}/{pageNumber}/{pageSize}")]
        [HttpGet]
        public async Task<HttpResponseMessage> Get(string search, int pageNumber, int pageSize)
        {
            try
            {
                if (pageNumber < 1 || pageSize < 1)
                {
                    pageSize = 1;
                    pageNumber = 1;
                }

                GenericPaging filter = new GenericPaging(pageNumber, pageSize);

                ICollection<TodoModel> result = Mapper.Map<ICollection<TodoModel>>(await service.GetRangeAsync(filter, search));

                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }



        [Authorize]
        [HttpGet]
        [Route("GetByUser/{userId}/{pageNumber}/{pageSize}")]
       public async Task<HttpResponseMessage> GetByUser(string userId, int pageNumber, int pageSize)
        {
            try
            {
                if (pageNumber < 1 || pageSize < 1)
                {
                    pageSize = 1;
                    pageNumber = 1;
                }

                GenericPaging filter = new GenericPaging(pageNumber, pageSize);

                ICollection<TodoModel> result = Mapper.Map<ICollection<TodoModel>>(await service.GetTaskAsync(filter, userId));

                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    

        /// <summary>
        /// Insert new task/todo
        /// </summary>
        /// <param name="todo">Todo model</param>
        /// <returns>The number of objects written to the underlying database if OK.</returns>
        [Authorize]
        [HttpPost]
        public async Task<HttpResponseMessage> Insert(TodoModel todo)
        {
            try
            {
                int result = await service.AddAsync(Mapper.Map<ITodo>(todo));

                if (result == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Add operation error.");
                }
                    
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        /// <summary>
        /// Update todo
        /// </summary>
        /// <param name="id">Todo id</param>
        /// <param name="model">Model to upadate</param>
        /// <returns>Http response</returns>
        [Authorize]
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<HttpResponseMessage> Update(Guid id, TodoModel model)
        {
            try
            {
                int result = await service.UpdateAsync(Mapper.Map<ITodo>(model));

                if (result >= 1)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, model);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Error while trying to edit task.");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        /// <summary>
        /// Delete task/todo
        /// </summary>
        /// <param name="id">Post id</param>
        /// <returns>Http response</returns>
        [HttpDelete]
        [Authorize]
        [Route("Delete/{id}")]
        public async Task<HttpResponseMessage> Delete(Guid id)
        {
            try
            {
                int result = await service.DeleteTodo(id);

                if (result == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Delete operation failed.");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, "Task deleted");
                }
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        #endregion
    }
}
