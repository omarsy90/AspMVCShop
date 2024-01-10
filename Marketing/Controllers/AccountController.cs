using Marketing.Areas.Identity.Data;
using Marketing.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Marketing.Controllers;




public class AccountController : Controller
{

    private readonly UserManager<MarketingUser> _userManager;
    private readonly SignInManager<MarketingUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ICustomerRepository _customerRepository;

    private readonly IEmailSender _emailSender;



    public AccountController(UserManager<MarketingUser> usermanager, SignInManager<MarketingUser> signInManager,ICustomerRepository customerRepository ,IEmailSender emailSender, RoleManager<IdentityRole> roleManager)
    {
        _userManager = usermanager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _customerRepository = customerRepository;
        _emailSender = emailSender;
    }






    [HttpGet]
    public IActionResult Register()
    {
        

        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {

        if (!ModelState.IsValid)
        {
            return View(model);
        }


        var user = await _userManager.FindByEmailAsync(model.Email);

        if(user is not null )
        {
            ModelState.AddModelError(string.Empty, "the user has been already registered !!");

            return View(model);
        }


        //identityuser
        MarketingUser marketingUser = new MarketingUser()
        {
            Email = model.Email,
            UserName = model.Email
        };
        
        var result = await _userManager.CreateAsync(marketingUser,model.Password);

        if (!result.Succeeded)
        {
            

            foreach(var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);

        }


        var role = await _roleManager.FindByNameAsync("Default");

        await _userManager.AddToRoleAsync(marketingUser, role.Name);
        

        Customer customer = new Customer()
        {
            ID = marketingUser.Id,
            CustomerFullName = model.FullName,
            CustomerCountry = model.Country,
            CustomerState = model.State,
            AppartmentNumber = model.DoorNumber,
            TelefonNumber = model.TelefonNumber,
            Email = model.Email,
            CustomerStreet = model.Street,

        };

          _customerRepository.AddCustomer(customer);





        string code = await _userManager.GenerateEmailConfirmationTokenAsync(marketingUser);
        var callback =   Url.Action(nameof(ConfirmEmail), "AccountTest", new {userId = marketingUser.Id , code = code},protocol:HttpContext.Request.Scheme);

       await _emailSender.SendEmailAsync("alknjomar6@gmail.com", model.Email, "ConfirmEmail", callback);

        return View("ConfirmEmail");

    }

    
    public IActionResult Login()
    {
        return View("login");
    }


    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) { return View(model); }

        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user is null || ! await _userManager.CheckPasswordAsync(user, model.Password))
        {
            ModelState.AddModelError("", "email or passwoed is incorrect !!");

            return View(model);
        }


        var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: true);

        if (!result.Succeeded)
        {
            ModelState.AddModelError("", " erroe while log in");
            return View(model);
        }

        return RedirectToAction("index", "Home");



    }


    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

       return  RedirectToAction("index", "Home");

    }


   



    [HttpGet]
    public async Task<IActionResult> ConfirmEmail(string userId ,string code)
    {
        if(string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(code))
        {
            return BadRequest();
        }

        var user = await _userManager.FindByIdAsync(userId);
        if(user is null)
        {
            BadRequest();
        }

        var result = await _userManager.ConfirmEmailAsync(user, code);

        if (!result.Succeeded)
        {
            BadRequest();
        }

        return View("SuccessEmailConfirmation");

    }




}

