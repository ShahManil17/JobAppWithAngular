using JobApplicationAPIs.Data;
using JobApplicationAPIs.Data.DataModel;
using JobApplicationAPIs.Model;
using JobApplicationAPIs.Model.ResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace JobApplicationAPIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Policy = "ShowAll")]
        [HttpGet("ShowBasicDetails")]
        public BasicDetailsViewModelResponse ShowAll()
        {
            var test = new BasicDetailsViewModelResponse();
            try
            {
                var data = _context.BasicDetails.FromSqlRaw("EXEC getAllData").ToList();
                test.IsSucess = true;
                test.BasicDetails = data;
                return test;
            }
            catch (Exception ex)
            {
                test.IsSucess = false;
                test.ErrorMessgae = ex.ToString();
                return test;
            }

        }

        //[Authorize(Policy = "ShowAll")]
        [HttpGet("ShowAllDetails")]
        public async Task<IActionResult> showAllDetails()
        {
            try
            {
                var dataModel = await _context.Database.SqlQuery<string>($"EXEC getAllDetails").ToListAsync();
                List<ShowAllViewModel> showAllViewModel;
                showAllViewModel = JsonSerializer.Deserialize<List<ShowAllViewModel>>(dataModel.First());
                var returnData = new ShowAllDetails
                {
                    Details = showAllViewModel,
                    IsSucess = true
                };

                return Ok(returnData);
            }
            catch
            {
                return BadRequest(new ShowAllDetails { IsSucess = false, ErrorMessgae = "Opps, Something Went Wrong" });
            }
        }

        [Authorize]
        [HttpPost("Apply")]
        public IActionResult Apply(ApplicationViewModel model)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    BasicDetails basicModel = new BasicDetails()
                    {
                        Name = model.Name,
                        Email = model.Email,
                        Address = model.Address,
                        PhoneNo = model.PhoneNo,
                        Gender = model.Gender,
                        RelationshipStatus = model.RelationshipStatus
                    };
                    _context.BasicDetails.Add(basicModel);
                    _context.SaveChanges();

                    int basicId = basicModel.Id;

                    foreach (var item in model.EducationDetails)
                    {
                        EducationDetails educationModel = new EducationDetails()
                        {
                            BasicDetailsId = basicId,
                            EducationLevel = item.EducationLevel,
                            BoardName = item.BoardName,
                            Percentage = item.Percentage,
                            PassingYear = item.PassingYear
                        };
                        _context.EducationDetails.Add(educationModel);
                    }

                    for (int i = 0; i < model.Company.Count; i++)
                    {
                        WorkExperience workExperience = new WorkExperience();
                        workExperience.BasicDetailsId = basicId;
                        workExperience.Company = model.Company[i];
                        workExperience.Designation = model.Designation[i];
                        workExperience.StartDate = model.From[i];
                        workExperience.EndDate = model.To[i];
                        _context.WorkExperiences.Add(workExperience);
                    }

                    string[] langArr = new string[] { "hindi", "english", "gujarati" };
                    string[] knownArr = new string[] { "read", "write", "speak" };

                    for (int i = 0; i < 3; i++)
                    {
                        Languages langModel = new Languages();
                        if (model.LangName[i] == null)
                        {
                            langModel.BasicDetailsId = basicId;
                            langModel.LanguageName = langArr[i];
                            string langLevel = "";
                            for (int j = 0; j < 3; j++)
                            {
                                if (model.LangLevel[i][j] == null)
                                {
                                    langLevel += knownArr[j] + ",";
                                }
                            }
                            if (langLevel != "")
                            {
                                langLevel = langLevel.TrimEnd(',');
                            }
                            langModel.LanguageLevel = langLevel;
                            _context.Languages.Add(langModel);
                        }
                    }

                    string[] techName = new string[] { "php", "mysql", "oracle", "laravel" };
                    for (int i = 0; i < 4; i++)
                    {
                        Technologies techModel = new Technologies();
                        if (model.TechName[i] == null)
                        {
                            techModel.BasicDetailsId = basicId;
                            techModel.TechnologyName = techName[i];
                            switch (techName[i])
                            {
                                case "php":
                                    techModel.TechnologyLevel = model.PhpLevel;
                                    break;
                                case "mysql":
                                    techModel.TechnologyLevel = model.MysqlLevel;
                                    break;
                                case "oracle":
                                    techModel.TechnologyLevel = model.OracleLevel;
                                    break;
                                case "laravel":
                                    techModel.TechnologyLevel = model.LaravelLevel;
                                    break;
                            }
                            _context.Technologies.Add(techModel);
                        }
                    }

                    string locationIns = "";
                    for (int i = 0; i < model.Location.Count; i++)
                    {
                        locationIns += model.Location[i] + ",";
                    }
                    locationIns = locationIns.TrimEnd(',');
                    Preferences prefModel = new Preferences();
                    prefModel.BasicDetailsId = basicId;
                    prefModel.Location = locationIns;
                    prefModel.Notice = model.Notice;
                    prefModel.ExpectedCtc = model.ExpectedCtc;
                    prefModel.CurrentCtc = model.CurrentCtc;
                    prefModel.Department = model.Department;
                    _context.Preferences.Add(prefModel);
                    _context.SaveChanges();

                    transaction.Commit();

                    return Ok(new ApplyResponse { Details = model, IsSucess = true });
                }
                catch
                {
                    transaction.Rollback();

                    return BadRequest(new ApplyResponse { Details = model, IsSucess = true, ErrorMessgae = "Opps, Something Went Wrong" });
                }
            }
        }

        [Authorize(Policy = "Update")]
        [HttpPut("Update")]
        public IActionResult Update(int userId, [FromBody] ApplicationViewModel model)
        {
            var checkId = _context.Users
                .Where(x => x.Id == userId)
                .Select(x => x.Id)
                .FirstOrDefault();
            if (checkId != null)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        // Update Basic Details
                        var id = _context.BasicDetails
                            .Where(x => x.Id == userId)
                            .Select(x => x.Id)
                            .FirstOrDefault();
                        BasicDetails basicModel = new BasicDetails()
                        {
                            Id = id,
                            Name = model.Name,
                            Email = model.Email,
                            Address = model.Address,
                            PhoneNo = model.PhoneNo,
                            Gender = model.Gender,
                            RelationshipStatus = model.RelationshipStatus
                        };
                        _context.BasicDetails.Update(basicModel);
                        _context.SaveChanges();

                        //Delete Old Work Education
                        var educationItems = _context.EducationDetails
                            .Where(x => x.BasicDetailsId == userId);
                        _context.EducationDetails.RemoveRange(educationItems);
                        _context.SaveChanges();

                        foreach (var item in model.EducationDetails)
                        {
                            if (item.BoardName != null)
                            {
                                EducationDetails educationModel = new EducationDetails()
                                {
                                    BasicDetailsId = userId,
                                    EducationLevel = item.EducationLevel,
                                    BoardName = item.BoardName,
                                    Percentage = item.Percentage,
                                    PassingYear = item.PassingYear
                                };
                                _context.EducationDetails.Add(educationModel);
                                _context.SaveChanges();
                            }
                        }

                        //Delete Old Work Experience
                        var expItems = _context.WorkExperiences
                            .Where(x => x.BasicDetailsId == userId);
                        _context.WorkExperiences.RemoveRange(expItems);
                        _context.SaveChanges();

                        //Add Updated Work Experience
                        for (int i = 0; i < model.Company.Count; i++)
                        {
                            if (model.Company[i] != null)
                            {
                                WorkExperience workExperience = new WorkExperience();
                                workExperience.BasicDetailsId = userId;
                                workExperience.Company = model.Company[i];
                                workExperience.Designation = model.Designation[i];
                                workExperience.StartDate = model.From[i];
                                workExperience.EndDate = model.To[i];
                                _context.WorkExperiences.Add(workExperience);
                                _context.SaveChanges();
                            }
                        }

                        //Delete Old Technologies
                        var techItems = _context.Technologies.Where(x => x.BasicDetailsId == userId);
                        _context.Technologies.RemoveRange(techItems);
                        _context.SaveChanges();

                        //Add updated Technologies
                        string[] techName = new string[] { "php", "mysql", "oracle", "laravel" };
                        for (int i = 0; i < 4; i++)
                        {
                            Technologies techModel = new Technologies();
                            if (model.TechName[i] == null)
                            {
                                techModel.BasicDetailsId = userId;
                                techModel.TechnologyName = techName[i];
                                switch (techName[i])
                                {
                                    case "php":
                                        techModel.TechnologyLevel = model.PhpLevel;
                                        break;
                                    case "mysql":
                                        techModel.TechnologyLevel = model.MysqlLevel;
                                        break;
                                    case "oracle":
                                        techModel.TechnologyLevel = model.OracleLevel;
                                        break;
                                    case "laravel":
                                        techModel.TechnologyLevel = model.LaravelLevel;
                                        break;
                                }
                                _context.Technologies.Add(techModel);
                                _context.SaveChanges();
                            }
                        }

                        //Delete Old Language Details
                        var langItems = _context.Languages.Where(x => x.BasicDetailsId == userId);
                        _context.Languages.RemoveRange(langItems);
                        _context.SaveChanges();

                        //Add Updated Language Details
                        string[] langArr = new string[] { "hindi", "english", "gujarati" };
                        string[] knownArr = new string[] { "read", "write", "speak" };
                        for (int i = 0; i < 3; i++)
                        {
                            Languages langModel = new Languages();
                            if (model.LangName[i] == null)
                            {
                                langModel.BasicDetailsId = userId;
                                langModel.LanguageName = langArr[i];
                                string langLevel = "";
                                for (int j = 0; j < 3; j++)
                                {
                                    if (model.LangLevel[i][j] == null)
                                    {
                                        langLevel += knownArr[j] + ",";
                                    }
                                }
                                if (langLevel != "")
                                {
                                    langLevel = langLevel.TrimEnd(',');
                                }
                                langModel.LanguageLevel = langLevel;
                                _context.Languages.Add(langModel);
                                _context.SaveChanges();
                            }
                        }

                        //Update Preferences Details
                        string locationIns = "";
                        for (int i = 0; i < model.Location.Count; i++)
                        {
                            locationIns += model.Location[i] + ",";
                        }
                        locationIns = locationIns.TrimEnd(',');
                        id = _context.Preferences
                            .Where(x => x.BasicDetailsId == userId)
                            .Select(x => x.Id)
                            .FirstOrDefault();
                        Preferences prefModel = new Preferences();
                        prefModel.Id = id;
                        prefModel.BasicDetailsId = userId;
                        prefModel.Location = locationIns;
                        prefModel.Notice = model.Notice;
                        prefModel.ExpectedCtc = model.ExpectedCtc;
                        prefModel.CurrentCtc = model.CurrentCtc;
                        prefModel.Department = model.Department;
                        _context.Preferences.Update(prefModel);
                        _context.SaveChanges();

                        //If Everything is right Commit the changes to the database
                        transaction.Commit();
                        return Ok(new ApplyResponse { Details = model, IsSucess = true});
                    }
                    catch
                    {
                        //handle exception and Rollback to prevent data loss
                        transaction.Rollback();

                        return BadRequest(new ApplyResponse { Details = model, IsSucess = true, ErrorMessgae = "Opps, Somethind Went Wrong" });
                    }
                }
            }
            else
            {
                return NotFound("Invalid Id Entered");
            }
        }

        [Authorize(Policy = "Delete")]
        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            var checkId = _context.Users
                .Where(x => x.Id == id)
                .Select(x => x.Id)
                .FirstOrDefault();
            if (checkId != null)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var eduItems = _context.EducationDetails.Where(x => x.BasicDetailsId == id);
                        _context.EducationDetails.RemoveRange(eduItems);

                        var expItems = _context.WorkExperiences.Where(x => x.BasicDetailsId == id);
                        _context.WorkExperiences.RemoveRange(expItems);

                        var techItems = _context.Technologies.Where(x => x.BasicDetailsId == id);
                        _context.Technologies.RemoveRange(techItems);

                        var langItems = _context.Languages.Where(x => x.BasicDetailsId == id);
                        _context.Languages.RemoveRange(langItems);

                        _context.Preferences.Remove(_context.Preferences.Where(x => x.BasicDetailsId == id).FirstOrDefault());

                        _context.BasicDetails.Remove(_context.BasicDetails.Where(x => x.Id == id).FirstOrDefault());
                        _context.SaveChanges();

                        transaction.Commit();

                        return Ok("Data Deleted Succesfully");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        return BadRequest("Opps, Something Went Wrong");
                    }
                }
            }
            else
            {
                return NotFound("Invalid Id Entered");
            }
                
        }
    }
}
