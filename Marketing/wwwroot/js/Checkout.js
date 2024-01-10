


const checkoutbtn = document.getElementById("checkout");

function GetElements() {

    const shopingCradDiv = document.querySelector(".shopingItemsDiv");
    let shopingCardsItems = [];
    shopingCardsItems[0] = [];
    shopingCardsItems[1] = [];
    console.log("shopingCarddiv : " + shopingCradDiv.textContent)

    let ProductIdElemets = shopingCradDiv.getElementsByClassName("ProductId");

    console.log("ProductIdElemets : " + ProductIdElemets.length);

    let idproductArray = [];
    for (let j = 0; j < ProductIdElemets.length; j++) {
        idproductArray[j] = ProductIdElemets[j].value;
    
    }

    let QuntityproductArray = [];
  
    let ProductsQuntityElements = shopingCradDiv.getElementsByClassName("Quantity");

    console.log("ProductsQuntityElements : " + ProductsQuntityElements.length);


    for (let j = 0; j < ProductsQuntityElements.length; j++) {
        QuntityproductArray[j] = ProductsQuntityElements[j].value;
     
    }



    
    for (let j = 0; j < idproductArray.length; j++)
    {
        shopingCardsItems[0][j] = idproductArray[j];
        }


    for (let j = 0; j < QuntityproductArray.length; j++)
    {
        shopingCardsItems[1][j] = QuntityproductArray[j];
    }

    return shopingCardsItems;

}













checkoutbtn.addEventListener("click", (event) => {

    let purches = GetElements()
    // purches[0] contaions The Ids
    // purches[1] contains the Qantitys
    Checkout(purches[0], purches[1]);
});


function Checkout(ids,quantities)
{
    let data = {ids,quantities}

    fetch(`${BaicUrl}/ShopingCard/CreatCheckoutSession/${data}`, {
        method: "POST", // or 'PUT'
        headers: {
         //   "Content-Type": "application/json",
        },
   
    })
        .then((response) => {
            if (response.redirected == true) {
                window.location.href = `${BaicUrl}/Identity/Account/Login`;
            }

            response.json()
        }

        )
        .then((data) => {
            if (data.status == 200) {
                window.location.href = `${data.url}`;

            }
            else {
                window.location.href = `${BaicUrl}/Home/Error`

            }

        })
        .catch((error) => {
            console.error("Error:", error);
        });




}

 