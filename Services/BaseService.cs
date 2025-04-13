using CarRentalManagementAPI.Models.Contexts;
using CarRentalManagementAPI.Models.DTOs;
using CarRentalManagementAPI.Models.DTOs.Customer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRentalManagementAPI.Services
{
    /// <summary>
    /// Service class that performs activities related with database
    /// </summary>
    /// <typeparam name="Model">Class of the exact object, related with database</typeparam>
    /// <typeparam name="ModelDTO">Class of the DTO object, this model will be communication body</typeparam>
    public abstract class BaseService<Model, ModelDTO>
        where Model : class, new()
        where ModelDTO: class, IBaseDTO, new()
    {
        protected DatabaseContext _context { get; set; } = default!;

        protected BaseService(DatabaseContext dbContext)
        {
            _context = dbContext;
        }

        /// <summary>
        /// Method returns List of items in DTO format
        /// </summary>
        /// <returns>List of items in DTO format</returns>
        public abstract Task<ActionResult<IEnumerable<ModelDTO>>> GetDtoList();

        /// <summary>
        /// Method returns Item with specific Id in DTO format
        /// </summary>
        /// <param name="id">Id of item to be returned</param>
        /// <returns>Item with specific Id in DTO format</returns>
        public abstract Task<ActionResult<ModelDTO>> GetDtoItemById(int id);

        /// <summary>
        /// Update item by Id
        /// </summary>
        /// <param name="id">Id of database item</param>
        /// <param name="modelDto">Edited DTO model object</param>
        public abstract Task<IActionResult> UpdateItem(int id, ModelDTO modelDto);

        /// <summary>
        /// Method performs adding new specific Record to the database
        /// </summary>
        /// <param name="modelDto">Model in DTO format to be added</param>
        /// <returns></returns>
        public abstract Task<ActionResult<ModelDTO>> CreateItem(ModelDTO modelDto);

        /// <summary>
        /// Method performs soft delete,
        /// It sets IsActive field to false and assign DeleteDateTime to now.
        /// </summary>
        /// <param name="id">Id of the record to be deleted</param>
        /// <returns></returns>
        public virtual async Task<IActionResult> DeleteItem(int id)
        {
            var item = await _context.Set<Model>().FindAsync(id);
            if (item == null) return new NotFoundResult();

            var propertyInfo = item.GetType().GetProperty("IsActive");
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(item, false);
            }

            propertyInfo = item.GetType().GetProperty("DeleteDateTime");
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(item, DateTime.Now);
            }

            await _context.SaveChangesAsync();

            return new NoContentResult();
        }

        /// <summary>
        /// Method search in database for record with desired Id and returns if found
        /// </summary>
        /// <param name="id">Id of item to be found</param>
        /// <returns>Returns true if record found in database, otherwise false</returns>
        public bool CheckIfExist(int id)
        {
            return _context.Set<Model>().Any(e => EF.Property<int>(e, "Id") == id);
        }

        /// <summary>
        /// Method check if parsed Id and Model in DTO format have the same IDs
        /// </summary>
        /// <param name="id">Id of the record</param>
        /// <param name="modelDTO">Model to be compared</param>
        /// <returns>Returns true if model and Id is the same, otherwise false</returns>
        public bool IsCorrectRequest(int id, ModelDTO modelDTO)
        {
            return id == modelDTO.Id;
        }
    }
}
