﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using RestSharp;
using Acumatica.RESTClient.Client;
using System.Threading;
using Acumatica.RESTClient.Model;

namespace Acumatica.RESTClient.Api
{
    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public abstract class EntityAPI<EntityType> : BaseApi
        where EntityType : Entity
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityAPI"/> class.
        /// </summary>
        /// <returns></returns>
        public EntityAPI(String basePath)
        {
            this.Configuration = new Configuration(basePath);

            ExceptionFactory = Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityAPI"/> class
        /// </summary>
        /// <returns></returns>
        public EntityAPI()
        {
            this.Configuration = Configuration.Default;

            ExceptionFactory = Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityAPI"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public EntityAPI(Configuration configuration)
        {
            if (configuration == null) // use the default one in Configuration
                this.Configuration = Configuration.Default;
            else
                this.Configuration = configuration;

            ExceptionFactory = Configuration.DefaultExceptionFactory;
        }
        #endregion

        #region Public Methods
        public void WaitActionCompletion(string invokeResult)
        {
            while (true)
            {
                var processResult = GetProcessStatus(invokeResult);

                switch (processResult)
                {
                    case 204:
                        return;
                    case 202:
                        Thread.Sleep(1000);
                        continue;
                    default:
                        throw new InvalidOperationException();
                }
            }
        }

        /// <summary>
        /// Retrieves records that satisfy the specified conditions from the system. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="select">The fields of the entity to be returned from the system. (optional)</param>
        /// <param name="filter">The conditions that determine which records should be selected from the system. (optional)</param>
        /// <param name="expand">The linked and detail entities that should be expanded. (optional)</param>
        /// <param name="custom">The fields that are not defined in the contract of the endpoint to be returned from the system. (optional)</param>
        /// <param name="skip">The number of records to be skipped from the list of returned records. (optional)</param>
        /// <param name="top">The number of records to be returned from the system. (optional)</param>
        /// <returns>Task of List&lt;Entity&gt;</returns>
        public async System.Threading.Tasks.Task<List<EntityType>> GetListAsync(string select = null, string filter = null, string expand = null, string custom = null, int? skip = null, int? top = null)
        {
            ApiResponse<List<EntityType>> localVarResponse = await GetListAsyncWithHttpInfo(select, filter, expand, custom, skip, top);
            return localVarResponse.Data;

        }

        /// <summary>
        /// Creates a record or updates an existing record if <paramref name="entity"/> can be mathed to an existing record by
        /// <c>id</c> field value or key fields values.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="entity">The record to be passed to the system.</param>
        /// <param name="select">The fields of the entity to be returned from the system. (optional)</param>
        /// <param name="filter">The conditions that determine which records should be selected from the system. (optional)</param>
        /// <param name="expand">The linked and detail entities that should be expanded. (optional)</param>
        /// <param name="custom">The fields that are not defined in the contract of the endpoint to be returned from the system. (optional)</param>
        /// <returns>EntityType</returns>
        public EntityType PutEntity(EntityType entity, string select = null, string filter = null, string expand = null, string custom = null)
        {
            ApiResponse<EntityType> localVarResponse = PutEntityWithHttpInfo(entity, select, filter, expand, custom);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Creates a record or updates an existing record if <paramref name="entity"/> can be mathed to an existing record by
        /// <c>id</c> field value or key fields values. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="entity">The record to be passed to the system.</param>
        /// <param name="select">The fields of the entity to be returned from the system. (optional)</param>
        /// <param name="filter">The conditions that determine which records should be selected from the system. (optional)</param>
        /// <param name="expand">The linked and detail entities that should be expanded. (optional)</param>
        /// <param name="custom">The fields that are not defined in the contract of the endpoint to be returned from the system. (optional)</param>
        /// <returns>Task of Entity</returns>
        public async System.Threading.Tasks.Task<EntityType> PutEntityAsync(EntityType entity, string select = null, string filter = null, string expand = null, string custom = null)
        {
            ApiResponse<EntityType> localVarResponse = await PutEntityAsyncWithHttpInfo(entity, select, filter, expand, custom);
            return localVarResponse.Data;

        }

        /// <summary>
        /// Attaches a file to a record. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="ids">The values of the key fields of the record.</param>
        /// <param name="filename">The name of the file that you are going to attach with the extension.</param>
        /// <returns></returns>
        public void PutFile(List<string> ids, string filename)
        {
            PutFileWithHttpInfo(ids, filename);
        }
        /// <summary>
        /// Attaches a file to a record. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="ids">The values of the key fields of the record.</param>
        /// <param name="filename">The name of the file that you are going to attach with the extension.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task PutFileAsync(List<string> ids, string filename)
        {
            await PutFileAsyncWithHttpInfo(ids, filename);

        }

        /// <summary>
        /// Performs an action in the system. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="action">The record to which the action should be applied and the parameters of the action.</param>
        /// <returns></returns>
        public string InvokeAction(EntityAction<EntityType> action)
        {
            var result = InvokeActionWithHttpInfo(action);
            return result.Headers["Location"];
        }

        /// <summary>
        /// Performs an action in the system. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="action">The record to which the action should be applied and the parameters of the action.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task InvokeActionAsync(EntityAction<EntityType> action)
        {
            await InvokeActionAsyncWithHttpInfo(action);

        }
        /// <summary>
        /// Retrieves a record by the values of its key fields from the system. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="ids">The values of the key fields of the record.</param>
        /// <param name="select">The fields of the entity to be returned from the system. (optional)</param>
        /// <param name="filter">The conditions that determine which records should be selected from the system. (optional)</param>
        /// <param name="expand">The linked and detail entities that should be expanded. (optional)</param>
        /// <param name="custom">The fields that are not defined in the contract of the endpoint to be returned from the system. (optional)</param>
        /// <returns>Task of Entity</returns>
        public async System.Threading.Tasks.Task<EntityType> GetByKeysAsync(List<string> ids, string select = null, string filter = null, string expand = null, string custom = null)
        {
            ApiResponse<EntityType> localVarResponse = await GetByKeysAsyncWithHttpInfo(ids, select, filter, expand, custom);
            return localVarResponse.Data;

        }

        /// <summary>
        /// Retrieves records that satisfy the specified conditions from the system. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="select">The fields of the entity to be returned from the system. (optional)</param>
        /// <param name="filter">The conditions that determine which records should be selected from the system. (optional)</param>
        /// <param name="expand">The linked and detail entities that should be expanded. (optional)</param>
        /// <param name="custom">The fields that are not defined in the contract of the endpoint to be returned from the system. (optional)</param>
        /// <param name="skip">The number of records to be skipped from the list of returned records. (optional)</param>
        /// <param name="top">The number of records to be returned from the system. (optional)</param>
        /// <returns>List&lt;Entity&gt;</returns>
        public List<EntityType> GetList(string select = null, string filter = null, string expand = null, string custom = null, int? skip = null, int? top = null)
        {
            ApiResponse<List<EntityType>> localVarResponse = GetListWithHttpInfo(select, filter, expand, custom, skip, top);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Retrieves a record by the values of its key fields from the system. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="ids">The values of the key fields of the record.</param>
        /// <param name="select">The fields of the entity to be returned from the system. (optional)</param>
        /// <param name="filter">The conditions that determine which records should be selected from the system. (optional)</param>
        /// <param name="expand">The linked and detail entities that should be expanded. (optional)</param>
        /// <param name="custom">The fields that are not defined in the contract of the endpoint to be returned from the system. (optional)</param>
        /// <returns>Entity</returns>
        public EntityType GetByKeys(List<string> ids, string select = null, string filter = null, string expand = null, string custom = null)
        {
            ApiResponse<EntityType> localVarResponse = GetByKeysWithHttpInfo(ids, select, filter, expand, custom);
            return localVarResponse.Data;
        }


        /// <summary>
        /// Retrieves a record by the value of the session entity ID from the system. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">The session ID of the record.</param>
        /// <param name="select">The fields of the entity to be returned from the system. (optional)</param>
        /// <param name="filter">The conditions that determine which records should be selected from the system. (optional)</param>
        /// <param name="expand">The linked and detail entities that should be expanded. (optional)</param>
        /// <param name="custom">The fields that are not defined in the contract of the endpoint to be returned from the system. (optional)</param>
        /// <returns>Task of Entity</returns>
        public async System.Threading.Tasks.Task<EntityType> GetByIdAsync(Guid? id, string select = null, string filter = null, string expand = null, string custom = null)
        {
            ApiResponse<EntityType> localVarResponse = await GetByIdAsyncWithHttpInfo(id, select, filter, expand, custom);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Retrieves the schema of custom fields of the entity from the system. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <returns>Task of Entity</returns>
        public async System.Threading.Tasks.Task<EntityType> GetAdHocSchemaAsync()
        {
            ApiResponse<EntityType> localVarResponse = await GetAdHocSchemaAsyncWithHttpInfo();
            return localVarResponse.Data;

        }

        /// <summary>
        /// Retrieves a record by the value of the session entity ID from the system. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">The session ID of the record.</param>
        /// <param name="select">The fields of the entity to be returned from the system. (optional)</param>
        /// <param name="filter">The conditions that determine which records should be selected from the system. (optional)</param>
        /// <param name="expand">The linked and detail entities that should be expanded. (optional)</param>
        /// <param name="custom">The fields that are not defined in the contract of the endpoint to be returned from the system. (optional)</param>
        /// <returns>Entity</returns>
        public EntityType GetById(Guid? id, string select = null, string filter = null, string expand = null, string custom = null)
        {
            ApiResponse<EntityType> localVarResponse = GetByIdWithHttpInfo(id, select, filter, expand, custom);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Deletes the record by the values of its key fields. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="ids">The values of the key fields of the record.</param>
        /// <returns></returns>
        public void DeleteByKeys(List<string> ids)
        {
            DeleteByKeysWithHttpInfo(ids);
        }

        /// <summary>
        /// Deletes the record by the values of its key fields. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="ids">The values of the key fields of the record.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteByKeysAsync(List<string> ids)
        {
            await DeleteByKeysAsyncWithHttpInfo(ids);

        }

        /// <summary>
        /// Retrieves the schema of custom fields of the entity from the system. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <returns>Entity</returns>
        public EntityType GetAdHocSchema()
        {
            ApiResponse<EntityType> localVarResponse = GetAdHocSchemaWithHttpInfo();
            return localVarResponse.Data;
        }

        /// <summary>
        /// Deletes the record by its session identifier. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">The session ID of the record.</param>
        /// <returns></returns>
        public void DeleteById(Guid? id)
        {
            DeleteByIdWithHttpInfo(id);
        }

        /// <summary>
        /// Deletes the record by its session identifier. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">The session ID of the record.</param>
        /// <returns>Task of void</returns>
        public async System.Threading.Tasks.Task DeleteByIdAsync(Guid? id)
        {
            await DeleteByIdAsyncWithHttpInfo(id);

        }
        #endregion

        #region Implementation
        private struct Location
        {
            public string Site;
            public string Entity;
            public string EndpointName;
            public string EndpointVersion;
            public string EntityName;
            public string ActionName;
            public string Status;
            public string ID;
        }
        private Location ParseLocation(string location)
        {
            var result = new Location();

            var parts = location.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
            result.Site = parts[0];
            result.Entity = parts[1];
            result.EndpointName = parts[2];
            result.EndpointVersion = parts[3];
            result.EntityName = parts[4];
            result.ActionName = parts[5];
            result.Status = parts[6];
            result.ID = parts[7];
            return result;
        }

        public int GetProcessStatus(string invokeResult)
        {
            if (invokeResult == null)
                ThrowMissingParameter("GetProcessStatus", nameof(invokeResult));

            var parsedLocation = ParseLocation(invokeResult);
            var localVarPath = "/" + parsedLocation.EntityName + "/" + parsedLocation.ActionName + "/" + parsedLocation.Status + "/" + parsedLocation.ID;
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)this.Configuration.ApiClient.CallApi(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, ComposeAcceptHeaders(HeaderContentType.Json), localVarFormParams, localVarFileParams,
                localVarPathParams, ComposeContentHeaders(HeaderContentType.None));

            int localVarStatusCode = (int)localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("GetById", localVarResponse);
                if (exception != null) throw exception;
            }
            return localVarStatusCode;
        }

        /// <summary>
        /// Attaches a file to a record. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="ids">The values of the key fields of the record.</param>
        /// <param name="filename">The name of the file that you are going to attach with the extension.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        protected ApiResponse<Object> PutFileWithHttpInfo(List<string> ids, string filename)
        {
            // verify the required parameter 'ids' is set
            if (ids == null)
                ThrowMissingParameter("PutFile", nameof(ids));
            // verify the required parameter 'filename' is set
            if (filename == null)
                ThrowMissingParameter("PutFile", nameof(filename));

            var localVarPath = "/" + GetEntityName() + "/{ids}/files/{filename}";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            if (ids != null) localVarPathParams.Add("ids", this.Configuration.ApiClient.ParameterToString(ids)); // path parameter
            if (filename != null) localVarPathParams.Add("filename", this.Configuration.ApiClient.ParameterToString(filename)); // path parameter


            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)this.Configuration.ApiClient.CallApi(localVarPath,
                Method.PUT, localVarQueryParams, localVarPostBody, ComposeAcceptHeaders(HeaderContentType.Json), localVarFormParams, localVarFileParams,
                localVarPathParams, ComposeContentHeaders(HeaderContentType.None));

            int localVarStatusCode = (int)localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("PutFile", localVarResponse);
                if (exception != null) throw exception;
            }

            return GetResponseHeaders(localVarResponse, localVarStatusCode);
        }


        /// <summary>
        /// Attaches a file to a record. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="ids">The values of the key fields of the record.</param>
        /// <param name="filename">The name of the file that you are going to attach with the extension.</param>
        /// <returns>Task of ApiResponse</returns>
        protected async System.Threading.Tasks.Task<ApiResponse<Object>> PutFileAsyncWithHttpInfo(List<string> ids, string filename)
        {
            // verify the required parameter 'ids' is set
            if (ids == null)
                ThrowMissingParameter("PutFile", nameof(ids));
            // verify the required parameter 'filename' is set
            if (filename == null)
                ThrowMissingParameter("PutFile", nameof(filename));

            var localVarPath = "/" + GetEntityName() + "/{ids}/files/{filename}";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            if (ids != null) localVarPathParams.Add("ids", this.Configuration.ApiClient.ParameterToString(ids)); // path parameter
            if (filename != null) localVarPathParams.Add("filename", this.Configuration.ApiClient.ParameterToString(filename)); // path parameter


            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)await this.Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.PUT, localVarQueryParams, localVarPostBody, ComposeAcceptHeaders(HeaderContentType.Json), localVarFormParams, localVarFileParams,
                localVarPathParams, ComposeContentHeaders(HeaderContentType.None));

            int localVarStatusCode = (int)localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("PutFile", localVarResponse);
                if (exception != null) throw exception;
            }

            return GetResponseHeaders(localVarResponse, localVarStatusCode);
        }



        /// <summary>
        /// Performs an action in the system. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="action">The record to which the action should be applied and the parameters of the action.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        protected ApiResponse<Object> InvokeActionWithHttpInfo(EntityAction<EntityType> action)
        {
            // verify the required parameter 'action' is set
            if (action == null)
                ThrowMissingParameter("InvokeAction", nameof(action));

            var localVarPath = "/" + GetEntityName() + "/" + action.GetType().Name;
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)this.Configuration.ApiClient.CallApi(localVarPath,
                Method.POST, localVarQueryParams, ComposeBody(action), ComposeAcceptHeaders(HeaderContentType.Json), localVarFormParams, localVarFileParams,
                localVarPathParams, ComposeContentHeaders(HeaderContentType.Json));

            int localVarStatusCode = (int)localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("InvokeAction", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<Object>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                null);
        }



        /// <summary>
        /// Performs an action in the system. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="action">The record to which the action should be applied and the parameters of the action.</param>
        /// <returns>Task of ApiResponse</returns>
        protected async System.Threading.Tasks.Task<ApiResponse<Object>> InvokeActionAsyncWithHttpInfo(EntityAction<EntityType> action)
        {
            // verify the required parameter 'action' is set
            if (action == null)
                ThrowMissingParameter("InvokeAction", nameof(action));

            var localVarPath = "/" + GetEntityName() + "/" + action.GetType().Name;
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)await this.Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.POST, localVarQueryParams, ComposeBody(action), ComposeAcceptHeaders(HeaderContentType.Json), localVarFormParams, localVarFileParams,
                localVarPathParams, ComposeContentHeaders(HeaderContentType.Json));

            int localVarStatusCode = (int)localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("InvokeAction", localVarResponse);
                if (exception != null) throw exception;
            }

            return new ApiResponse<Object>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                null);
        }

        /// <summary>
        /// Creates a record or updates an existing record if <paramref name="entity"/> can be mathed to an existing record by
        /// <c>id</c> field value or key fields values.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="entity">The record to be passed to the system.</param>
        /// <param name="select">The fields of the entity to be returned from the system. (optional)</param>
        /// <param name="filter">The conditions that determine which records should be selected from the system. (optional)</param>
        /// <param name="expand">The linked and detail entities that should be expanded. (optional)</param>
        /// <param name="custom">The fields that are not defined in the contract of the endpoint to be returned from the system. (optional)</param>
        /// <returns>Task of ApiResponse (Entity)</returns>
        protected async System.Threading.Tasks.Task<ApiResponse<EntityType>> PutEntityAsyncWithHttpInfo(EntityType entity, string select = null, string filter = null, string expand = null, string custom = null)
        {
            // verify the required parameter 'entity' is set
            if (entity == null)
                ThrowMissingParameter("PutEntity", nameof(entity));

            var localVarPath = "/" + GetEntityName();
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();

            if (select != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$select", select)); // query parameter
            if (filter != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$filter", filter)); // query parameter
            if (expand != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$expand", expand)); // query parameter
            if (custom != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$custom", custom)); // query parameter


            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)await this.Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.PUT, localVarQueryParams, ComposeBody(entity), ComposeAcceptHeaders(HeaderContentType.Json), localVarFormParams, localVarFileParams,
                localVarPathParams, ComposeContentHeaders(HeaderContentType.Json));

            int localVarStatusCode = (int)localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("PutEntity", localVarResponse);
                if (exception != null) throw exception;
            }

            return DeserializeResponse(localVarResponse, localVarStatusCode);
        }


        /// <summary>
        /// Retrieves a record by the values of its key fields from the system. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="ids">The values of the key fields of the record.</param>
        /// <param name="select">The fields of the entity to be returned from the system. (optional)</param>
        /// <param name="filter">The conditions that determine which records should be selected from the system. (optional)</param>
        /// <param name="expand">The linked and detail entities that should be expanded. (optional)</param>
        /// <param name="custom">The fields that are not defined in the contract of the endpoint to be returned from the system. (optional)</param>
        /// <returns>Task of ApiResponse (Entity)</returns>
        protected async System.Threading.Tasks.Task<ApiResponse<EntityType>> GetByKeysAsyncWithHttpInfo(List<string> ids, string select = null, string filter = null, string expand = null, string custom = null)
        {
            // verify the required parameter 'ids' is set
            if (ids == null)
                ThrowMissingParameter("GetByKeys", nameof(ids));

            var localVarPath = "/" + GetEntityName() + "/{ids}";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;


            if (ids != null) localVarPathParams.Add("ids", this.Configuration.ApiClient.ParameterToString(ids)); // path parameter
            if (select != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$select", select)); // query parameter
            if (filter != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$filter", filter)); // query parameter
            if (expand != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$expand", expand)); // query parameter
            if (custom != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$custom", custom)); // query parameter


            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)await this.Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, ComposeAcceptHeaders(HeaderContentType.Json), localVarFormParams, localVarFileParams,
                localVarPathParams, ComposeContentHeaders(HeaderContentType.None));

            int localVarStatusCode = (int)localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("GetByKeys", localVarResponse);
                if (exception != null) throw exception;
            }

            return DeserializeResponse(localVarResponse, localVarStatusCode);
        }

        /// <summary>
        /// Retrieves records that satisfy the specified conditions from the system. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="select">The fields of the entity to be returned from the system. (optional)</param>
        /// <param name="filter">The conditions that determine which records should be selected from the system. (optional)</param>
        /// <param name="expand">The linked and detail entities that should be expanded. (optional)</param>
        /// <param name="custom">The fields that are not defined in the contract of the endpoint to be returned from the system. (optional)</param>
        /// <param name="skip">The number of records to be skipped from the list of returned records. (optional)</param>
        /// <param name="top">The number of records to be returned from the system. (optional)</param>
        /// <returns>Task of ApiResponse (List&lt;Entity&gt;)</returns>
        protected async System.Threading.Tasks.Task<ApiResponse<List<EntityType>>> GetListAsyncWithHttpInfo(string select = null, string filter = null, string expand = null, string custom = null, int? skip = null, int? top = null)
        {

            var localVarPath = "/" + GetEntityName();
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            if (select != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$select", select)); // query parameter
            if (filter != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$filter", filter)); // query parameter
            if (expand != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$expand", expand)); // query parameter
            if (custom != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$custom", custom)); // query parameter
            if (skip != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$skip", skip)); // query parameter
            if (top != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$top", top)); // query parameter


            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)await this.Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, ComposeAcceptHeaders(HeaderContentType.Json), localVarFormParams, localVarFileParams,
                localVarPathParams, ComposeContentHeaders(HeaderContentType.None));

            int localVarStatusCode = (int)localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("GetList", localVarResponse);
                if (exception != null) throw exception;
            }

            return DeserializeListOfEntities(localVarResponse, localVarStatusCode);
        }

        /// <summary>
        /// Retrieves a record by the values of its key fields from the system. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="ids">The values of the key fields of the record.</param>
        /// <param name="select">The fields of the entity to be returned from the system. (optional)</param>
        /// <param name="filter">The conditions that determine which records should be selected from the system. (optional)</param>
        /// <param name="expand">The linked and detail entities that should be expanded. (optional)</param>
        /// <param name="custom">The fields that are not defined in the contract of the endpoint to be returned from the system. (optional)</param>
        /// <returns>ApiResponse of Entity</returns>
        protected ApiResponse<EntityType> GetByKeysWithHttpInfo(List<string> ids, string select = null, string filter = null, string expand = null, string custom = null)
        {
            // verify the required parameter 'ids' is set
            if (ids == null)
                ThrowMissingParameter("GetByKeys", nameof(ids));

            var localVarPath = "/" + GetEntityName() + "/{ids}";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            if (ids != null) localVarPathParams.Add("ids", this.Configuration.ApiClient.ParameterToString(ids)); // path parameter
            if (select != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$select", select)); // query parameter
            if (filter != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$filter", filter)); // query parameter
            if (expand != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$expand", expand)); // query parameter
            if (custom != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$custom", custom)); // query parameter


            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)this.Configuration.ApiClient.CallApi(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, ComposeAcceptHeaders(HeaderContentType.Json), localVarFormParams, localVarFileParams,
                localVarPathParams, ComposeContentHeaders(HeaderContentType.None));

            int localVarStatusCode = (int)localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("GetByKeys", localVarResponse);
                if (exception != null) throw exception;
            }

            return DeserializeResponse(localVarResponse, localVarStatusCode);
        }

        /// <summary>
        /// Retrieves records that satisfy the specified conditions from the system. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="select">The fields of the entity to be returned from the system. (optional)</param>
        /// <param name="filter">The conditions that determine which records should be selected from the system. (optional)</param>
        /// <param name="expand">The linked and detail entities that should be expanded. (optional)</param>
        /// <param name="custom">The fields that are not defined in the contract of the endpoint to be returned from the system. (optional)</param>
        /// <param name="skip">The number of records to be skipped from the list of returned records. (optional)</param>
        /// <param name="top">The number of records to be returned from the system. (optional)</param>
        /// <returns>ApiResponse of List&lt;Entity&gt;</returns>
        protected ApiResponse<List<EntityType>> GetListWithHttpInfo(string select = null, string filter = null, string expand = null, string custom = null, int? skip = null, int? top = null)
        {

            var localVarPath = "/" + GetEntityName();
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            if (select != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$select", select)); // query parameter
            if (filter != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$filter", filter)); // query parameter
            if (expand != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$expand", expand)); // query parameter
            if (custom != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$custom", custom)); // query parameter
            if (skip != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$skip", skip)); // query parameter
            if (top != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$top", top)); // query parameter


            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)this.Configuration.ApiClient.CallApi(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, ComposeAcceptHeaders(HeaderContentType.Json), localVarFormParams, localVarFileParams,
                localVarPathParams, ComposeContentHeaders(HeaderContentType.None));

            int localVarStatusCode = (int)localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("GetList", localVarResponse);
                if (exception != null) throw exception;
            }
            return DeserializeListOfEntities(localVarResponse, localVarStatusCode);
        }

        private ApiResponse<List<EntityType>> DeserializeListOfEntities(IRestResponse localVarResponse, int localVarStatusCode)
        {
            return new ApiResponse<List<EntityType>>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (List<EntityType>)this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(List<EntityType>)));
        }

        /// <summary>
        /// Retrieves a record by the value of the session entity ID from the system. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">The session ID of the record.</param>
        /// <param name="select">The fields of the entity to be returned from the system. (optional)</param>
        /// <param name="filter">The conditions that determine which records should be selected from the system. (optional)</param>
        /// <param name="expand">The linked and detail entities that should be expanded. (optional)</param>
        /// <param name="custom">The fields that are not defined in the contract of the endpoint to be returned from the system. (optional)</param>
        /// <returns>Task of ApiResponse (Entity)</returns>
        protected async System.Threading.Tasks.Task<ApiResponse<EntityType>> GetByIdAsyncWithHttpInfo(Guid? id, string select = null, string filter = null, string expand = null, string custom = null)
        {
            // verify the required parameter 'id' is set
            if (id == null)
                ThrowMissingParameter("GetById", nameof(id));

            var localVarPath = "/" + GetEntityName() + "/{id}";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            if (id != null) localVarPathParams.Add("id", this.Configuration.ApiClient.ParameterToString(id)); // path parameter
            if (select != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$select", select)); // query parameter
            if (filter != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$filter", filter)); // query parameter
            if (expand != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$expand", expand)); // query parameter
            if (custom != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$custom", custom)); // query parameter


            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)await this.Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, ComposeAcceptHeaders(HeaderContentType.Json), localVarFormParams, localVarFileParams,
                localVarPathParams, ComposeContentHeaders(HeaderContentType.None));

            int localVarStatusCode = (int)localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("GetById", localVarResponse);
                if (exception != null) throw exception;
            }

            return DeserializeResponse(localVarResponse, localVarStatusCode);
        }


        /// <summary>
        /// Creates a record or updates an existing record if <paramref name="entity"/> can be mathed to an existing record by
        /// <c>id</c> field value or key fields values.
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="entity">The record to be passed to the system.</param>
        /// <param name="select">The fields of the entity to be returned from the system. (optional)</param>
        /// <param name="filter">The conditions that determine which records should be selected from the system. (optional)</param>
        /// <param name="expand">The linked and detail entities that should be expanded. (optional)</param>
        /// <param name="custom">The fields that are not defined in the contract of the endpoint to be returned from the system. (optional)</param>
        /// <returns>ApiResponse of Entity</returns>
        protected ApiResponse<EntityType> PutEntityWithHttpInfo(EntityType entity, string select = null, string filter = null, string expand = null, string custom = null)
        {
            if (entity == null)
                throw new ApiException(400, $"Missing required parameter '{nameof(entity)}' when calling EntityApi->PutEntity");

            var localVarPath = "/" + GetEntityName();
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();

            if (select != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$select", select)); // query parameter
            if (filter != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$filter", filter)); // query parameter
            if (expand != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$expand", expand)); // query parameter
            if (custom != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$custom", custom)); // query parameter

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)this.Configuration.ApiClient.CallApi(localVarPath,
                Method.PUT, localVarQueryParams, ComposeBody(entity), ComposeAcceptHeaders(HeaderContentType.Json), localVarFormParams, localVarFileParams,
                localVarPathParams, ComposeContentHeaders(HeaderContentType.Json));

            int localVarStatusCode = (int)localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("PutEntity", localVarResponse);
                if (exception != null) throw exception;
            }

            return DeserializeResponse(localVarResponse, localVarStatusCode);
        }

        /// <summary>
        /// Retrieves a record by the value of the session entity ID from the system. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">The session ID of the record.</param>
        /// <param name="select">The fields of the entity to be returned from the system. (optional)</param>
        /// <param name="filter">The conditions that determine which records should be selected from the system. (optional)</param>
        /// <param name="expand">The linked and detail entities that should be expanded. (optional)</param>
        /// <param name="custom">The fields that are not defined in the contract of the endpoint to be returned from the system. (optional)</param>
        /// <returns>ApiResponse of Entity</returns>
        protected ApiResponse<EntityType> GetByIdWithHttpInfo(Guid? id, string select = null, string filter = null, string expand = null, string custom = null)
        {
            // verify the required parameter 'id' is set
            if (id == null)
                ThrowMissingParameter("GetById", nameof(id));

            var localVarPath = "/" + GetEntityName() + "/{id}";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            if (id != null) localVarPathParams.Add("id", this.Configuration.ApiClient.ParameterToString(id)); // path parameter
            if (select != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$select", select)); // query parameter
            if (filter != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$filter", filter)); // query parameter
            if (expand != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$expand", expand)); // query parameter
            if (custom != null) localVarQueryParams.AddRange(this.Configuration.ApiClient.ParameterToKeyValuePairs("", "$custom", custom)); // query parameter


            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)this.Configuration.ApiClient.CallApi(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, ComposeAcceptHeaders(HeaderContentType.Json), localVarFormParams, localVarFileParams,
                localVarPathParams, ComposeContentHeaders(HeaderContentType.None));

            int localVarStatusCode = (int)localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("GetById", localVarResponse);
                if (exception != null) throw exception;
            }

            return DeserializeResponse(localVarResponse, localVarStatusCode);
        }

        /// <summary>
        /// Retrieves the schema of custom fields of the entity from the system. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <returns>Task of ApiResponse (Entity)</returns>
        protected async System.Threading.Tasks.Task<ApiResponse<EntityType>> GetAdHocSchemaAsyncWithHttpInfo()
        {

            var localVarPath = "/" + GetEntityName() + "/$adHocSchema";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)await this.Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, ComposeAcceptHeaders(HeaderContentType.Json), localVarFormParams, localVarFileParams,
                localVarPathParams, ComposeContentHeaders(HeaderContentType.None));

            int localVarStatusCode = (int)localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("GetAdHocSchema", localVarResponse);
                if (exception != null) throw exception;
            }
            return DeserializeResponse(localVarResponse, localVarStatusCode);
        }

        private ApiResponse<EntityType> DeserializeResponse(IRestResponse localVarResponse, int localVarStatusCode)
        {
            return new ApiResponse<EntityType>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                (EntityType)this.Configuration.ApiClient.Deserialize(localVarResponse, typeof(EntityType)));
        }

        /// <summary>
        /// Retrieves the schema of custom fields of the entity from the system. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <returns>ApiResponse of Entity</returns>
        protected ApiResponse<EntityType> GetAdHocSchemaWithHttpInfo()
        {

            var localVarPath = "/" + GetEntityName() + "/$adHocSchema";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)this.Configuration.ApiClient.CallApi(localVarPath,
                Method.GET, localVarQueryParams, localVarPostBody, ComposeAcceptHeaders(HeaderContentType.Json), localVarFormParams, localVarFileParams,
                localVarPathParams, ComposeContentHeaders(HeaderContentType.None));

            int localVarStatusCode = (int)localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("GetAdHocSchema", localVarResponse);
                if (exception != null) throw exception;
            }

            return DeserializeResponse(localVarResponse, localVarStatusCode);
        }

        /// <summary>
        /// Deletes the record by the values of its key fields. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="ids">The values of the key fields of the record.</param>
        /// <returns>Task of ApiResponse</returns>
        protected async System.Threading.Tasks.Task<ApiResponse<Object>> DeleteByKeysAsyncWithHttpInfo(List<string> ids)
        {
            // verify the required parameter 'ids' is set
            if (ids == null)
                ThrowMissingParameter("DeleteByKeys", nameof(ids));

            var localVarPath = "/" + GetEntityName() + "/{ids}";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            if (ids != null) localVarPathParams.Add("ids", this.Configuration.ApiClient.ParameterToString(ids)); // path parameter


            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)await this.Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.DELETE, localVarQueryParams, localVarPostBody, ComposeAcceptHeaders(HeaderContentType.Any), localVarFormParams, localVarFileParams,
                localVarPathParams, ComposeContentHeaders(HeaderContentType.None));

            int localVarStatusCode = (int)localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("DeleteByKeys", localVarResponse);
                if (exception != null) throw exception;
            }

            return GetResponseHeaders(localVarResponse, localVarStatusCode);
        }

        protected virtual string GetEntityName()
        {
            return typeof(EntityType).Name;
        }
        /// <summary>
        /// Deletes the record by the values of its key fields. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="ids">The values of the key fields of the record.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        protected ApiResponse<Object> DeleteByKeysWithHttpInfo(List<string> ids)
        {
            // verify the required parameter 'ids' is set
            if (ids == null)
                ThrowMissingParameter("DeleteByKeys", nameof(ids));

            var localVarPath = "/" + GetEntityName() + "/{ids}";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            if (ids != null) localVarPathParams.Add("ids", this.Configuration.ApiClient.ParameterToString(ids)); // path parameter


            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)this.Configuration.ApiClient.CallApi(localVarPath,
                Method.DELETE, localVarQueryParams, localVarPostBody, ComposeAcceptHeaders(HeaderContentType.Any), localVarFormParams, localVarFileParams,
                localVarPathParams, ComposeContentHeaders(HeaderContentType.None));

            int localVarStatusCode = (int)localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("DeleteByKeys", localVarResponse);
                if (exception != null) throw exception;
            }

            return GetResponseHeaders(localVarResponse, localVarStatusCode);
        }

        /// <summary>
        /// Deletes the record by its session identifier. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">The session ID of the record.</param>
        /// <returns>Task of ApiResponse</returns>
        protected async System.Threading.Tasks.Task<ApiResponse<Object>> DeleteByIdAsyncWithHttpInfo(Guid? id)
        {
            // verify the required parameter 'id' is set
            if (id == null)
                ThrowMissingParameter("DeleteById", nameof(id));

            var localVarPath = "/" + GetEntityName() + "/{id}";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            if (id != null) localVarPathParams.Add("id", this.Configuration.ApiClient.ParameterToString(id)); // path parameter


            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)await this.Configuration.ApiClient.CallApiAsync(localVarPath,
                Method.DELETE, localVarQueryParams, localVarPostBody, ComposeAcceptHeaders(HeaderContentType.Any), localVarFormParams, localVarFileParams,
                localVarPathParams, ComposeContentHeaders(HeaderContentType.None));

            int localVarStatusCode = (int)localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("DeleteById", localVarResponse);
                if (exception != null) throw exception;
            }

            return GetResponseHeaders(localVarResponse, localVarStatusCode);
        }


        /// <summary>
        /// Deletes the record by its session identifier. 
        /// </summary>
        /// <exception cref="ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">The session ID of the record.</param>
        /// <returns>ApiResponse of Object(void)</returns>
        protected ApiResponse<Object> DeleteByIdWithHttpInfo(Guid? id)
        {
            // verify the required parameter 'id' is set
            if (id == null)
                ThrowMissingParameter("DeleteById", nameof(id));

            var localVarPath = "/" + GetEntityName() + "/{id}";
            var localVarPathParams = new Dictionary<String, String>();
            var localVarQueryParams = new List<KeyValuePair<String, String>>();
            var localVarFormParams = new Dictionary<String, String>();
            var localVarFileParams = new Dictionary<String, FileParameter>();
            Object localVarPostBody = null;

            if (id != null) localVarPathParams.Add("id", this.Configuration.ApiClient.ParameterToString(id)); // path parameter


            // make the HTTP request
            IRestResponse localVarResponse = (IRestResponse)this.Configuration.ApiClient.CallApi(localVarPath,
                Method.DELETE, localVarQueryParams, localVarPostBody, ComposeAcceptHeaders(HeaderContentType.Any), localVarFormParams, localVarFileParams,
                localVarPathParams, ComposeContentHeaders(HeaderContentType.None));

            int localVarStatusCode = (int)localVarResponse.StatusCode;

            if (ExceptionFactory != null)
            {
                Exception exception = ExceptionFactory("DeleteById", localVarResponse);
                if (exception != null) throw exception;
            }
            return GetResponseHeaders(localVarResponse, localVarStatusCode);
        }

        private static ApiResponse<object> GetResponseHeaders(IRestResponse localVarResponse, int localVarStatusCode)
        {
            return new ApiResponse<Object>(localVarStatusCode,
                localVarResponse.Headers.ToDictionary(x => x.Name, x => x.Value.ToString()),
                null);
        }
        #endregion
    }
}
