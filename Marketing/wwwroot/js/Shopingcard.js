
const BaicUrl = 'https://localhost:7127';

async function AddToCrad(e) {

    let productId = e.target.id;

    await fetch(`${BaicUrl}/ShopingCard/addProduct/${productId}`, {
        method: 'POST',

        headers: {
            'content-Type': "application/json; charset=utf-8",
        }

    }).then((res) => {

        if (res.redirected === true) {
            window.location.href = `${BaicUrl}/Identity/Account/Login`;

        }

        return res.json();

    }).then(data => {



        if (data.status === 401) {
            //Unuthorized
            window.location.href = `${BaicUrl}/Identity/Account/Login`;
        }
        else if (data.status == 400) {
            //bad request
            window.alert(data.error);
        } else if (data.status == 200) {

            ClicCounter += 1;
            Animate();

            

        }

    }).catch(err => {

    });

    await displayShopingCardItem()

}



async function RemoveItemFromShpingCard(e)

{
    let productId = e.target.id;

    await fetch(`${BaicUrl}/ShopingCard/RemoveItem/${productId}`, {

        method: "Delete",
        headers: {
            'content-Type': "application/json; charset=utf-8",
        },

        body: JSON.stringify({ productId })

    }).then(res => {

        if (res.redirected === true) {

            window.location.href = `${BaicUrl}/Identity/Account/Login`;
        }

        return res.json();

    }).then(data => {

        if (data.status === 400) {

            window.alert("bad request 400 !");
        }
        else if (data.status === 401) {
            window.location.href = `${BaicUrl}/Identity/Account/Login`;
        }
    })

     await displayShopingCardItem();
}


async function GetShopingCardItems() {

   return  await fetch(`${BaicUrl}/ShopingCard/GetShopingCardItems`, {

        method:'GET'

    }).then(res => {
        return res.json();
    }).then(data => {

        if (data.status === 400) {

            //bad request
            window.alert("bad request 400");
        } else if (data.status == 401) {
            // unathorized 
            window.location.href = `${BaicUrl}/Identity/Account/Login`
        }

        return data;

    }).catch(err => {
        console.log("error while getting shoping card items !!!")
    })

}

let shopingItemsDiv = document.getElementsByClassName("shopingItemsDiv")[0];
let totalSpan = document.getElementById("total");

  async function displayShopingCardItem() {

      let str = "";
      let ShopingCard = await GetShopingCardItems().then(res => res);

      let ShopingCardItems = ShopingCard.itemsList;
      const total = ShopingCard.total;

      shopingItemsDiv.innerText = "";

      for (let i = 0; i < ShopingCardItems.length; i ++) {

          let item = ShopingCardItems[i];

          str += `<div class="col-sm-12 col-md-6 col-lg-4">


  <div class="card" style="margin-bottom:20px" >
    <img  height="400" src="/${item.productUrl}" class="card-img-top" alt="...">
       <div class="card-body">

           <input type="text" value="${item.productId}" style="visibility:hidden" class="ProductId"  />
            <input type="text" value="${item.quantity}" style="visibility:hidden" class="Quantity" />
           
            <h4>Unit Price :  <span class="label label-default">${item.unitPrice}</span></h4>
            <h4>Quantity  :  <span class="label label-default">${item.quantity}</span></h4>
            <h4>Total : <span class="label label-default">${item.totalPrice}</span></h4>
     
       </div>
       <div style="display:flex;flex-wrap:wrap;justify-content:space-between; background-color:green" >
            <button style="width:50px" type="button" id="${item.productId}" onclick="RemoveItemFromShpingCard(event)" > -  </button>
            <button style="width:50px" type="button" id="${item.productId}" onclick="AddToCrad(event)"> + </button>
       </div>
       
   </div>
</div>`



      }

      shopingItemsDiv.innerHTML = str;

      totalSpan.innerText = total;


}