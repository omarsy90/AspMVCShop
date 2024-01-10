

const BaicUrl = 'https://localhost:7127';


async function GetProductsByCategoryId(e) {

    let categoryId = e.target.id


    const products = await fetch(`${BaicUrl}/Category/GetProducts/${categoryId}`, {
        method: 'Get', // or 'PUT'
       
       
    })
        .then((response) => response.json())
        .then((data) => {
            return data
        })
        .catch((error) => {
            console.error('Error:', error);
        });


    console.log(products.length)
    DispayFilteringProducs(products);

}


let ClicCounter = 0;

function Animate() {


    const AnimatedHtmlelement = document.getElementById("shopingCradId");

    if (ClicCounter % 2 === 0) {
        AnimatedHtmlelement.classList.remove("animate__shakeY");
        AnimatedHtmlelement.classList.add("animate__shakeX")
    } else {

        AnimatedHtmlelement.classList.remove("animate__shakeX");
        AnimatedHtmlelement.classList.add("animate__shakeY");
    }
   
  


}





   async function AddToCrad(e) {

       let productId = e.target.id;

       await fetch(`${BaicUrl}/ShopingCard/addProduct/${productId}`, {
           method: 'POST',

           headers: {
               'content-Type': "application/json; charset=utf-8",
           }

       }).then((res) => {

           if (res.redirected === true) {
               window.location.href = `${BaicUrl}/Account/Login`;
               
           }

           return res.json();

       }).then(data => {

         
          
           if (data.status === 401) {
               //Unuthorized
               window.location.href = `${BaicUrl}/Account/Login`;
           }
           else if (data.status == 400) {
               //bad request
               window.alert(data.error);
           } else if (data.status == 201) {

               ClicCounter += 1;
               Animate();
              
           }
           
       }).catch(err => {
          
       })
         



    }


function GetUserId() {

    
}

function DispayFilteringProducs(products) {

 
    productsDiv = document.getElementById("productsArea");
    console.log(productsDiv)
    productsDiv.innerText = "";
    let htmlstr = '';
    for (let i = 0; i < products.length; i++) {
      
        let urlDetails = `Product/Details/${products[i].id}`;
        htmlstr += `<div class="col-sm-12 col-md-6 col-lg-4">
                       <div class="card" >
                             <img height="400" src="/${products[i].imgUrl}" class="card-img-top" alt="...">
                             <div class="card-body">
                                    <h5 class="card-title">${products[i].productName}</h5> 
                                    <p class="card-text">${products[i].productPrise}</p> 
                                    <a href= "`+ urlDetails +`" class="btn btn-primary">Details</a>
                            </div> 
                    <button type="button" class="btn btn-success" id="`+products[i].id+`" onclick="AddToCrad(event)">add to Shoping Card</button>
                      </div>
                  </div > `


    }

    productsDiv.innerHTML = htmlstr;
   

} 







