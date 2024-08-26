const categoriesAPI = "https://localhost:7229/api/Categories";

////////////////////////////////
// Method 1 using getallProducts

// const productsAPI = "https://localhost:7229/api/Products";

// async function myData() {

//     const categoriesData = await fetch(categoriesAPI);
//     let categoriesJSON = await categoriesData.json();

//     const productsData = await fetch(productsAPI);
//     let productsJSON = await productsData.json();

//     let catCon = document.getElementById("categoriesCards");
//     let proCon = document.getElementById("productsCards");
//     let deCon = document.getElementById("detailsCard");


//     if (catCon)
//     {
//         function categoriesCard() {
//             categoriesJSON.forEach((element) => {
//                 catCon.innerHTML += `
//                     <div class="col-lg-4" >
//                         <a onclick = "storeID(${element.categoryId})" href="products.html" style="text-decoration: none;">
//                             <div class="single-banner categorycard" >
//                                 <img src="${element.categoryImage}" alt="" />
//                                 <div class="inner-text">
//                                     <h4>${element.categoryName}</h4>
//                                 </div>
//                             </div>
//                         </a>
//                     </div>
//                 `;
//             });
//         };
//     categoriesCard();
//     };


//     if (proCon)
//     {
//         function productsCard() {
//             productsJSON.forEach((x) => {
//                 var localID = localStorage.getItem("categoryId");
//                 if (localID == null || x.categoryId == localID)
//                 {
//                     proCon.innerHTML += `
//                     <div class="col-md-4 mb-3">
//                         <div class="card" style="margin:10px;">
//                             <img class="card-img-top" src="${x.productImage}" alt=" " style="height:300px;">
//                             <div class="card-body">
//                                 <p>${x.productName}</p>
//                                 <p>${x.price} JOD</p>
//                                 <p>${x.description}</p>
//                             </div>
//                             <div class="card-footer">
//                                 <input type="submit" onclick="deatailId(${x.productId})" value="Details" class="btn" style="color: #283618" />
//                             </div>
//                         </div>
//                     </div>
//                 `;
//                 }
//             });
//         };
//         productsCard();
//         localStorage.clear();
//     };


//     if (deCon)
//     {
//         function detailsCard()
//         {

//             productsJSON.forEach((a) =>
//             {

//                 if (localStorage.getItem("productId")!== null && a.productId == localStorage.getItem("productId"))
//                 {
//                     deCon.innerHTML += `
//                                     <aside class="col-lg-6">
//                     <div class="border rounded-4 mb-3 d-flex justify-content-center">
//                         <a data-fslightbox="mygalley" class="rounded-4" target="_blank" data-type="image">
//                             <img style="max-width: 100%; max-height: 100vh; margin: auto;" class="rounded-4 fit" src="${a.productImage}" />
//                         </a>
//                     </div>
//                 </aside>
//                 <main class="col-lg-6">
//                     <div class="ps-lg-3">
//                         <h4 class="title text-dark">
//                             ${a.productName}
//                         </h4>
//                         <div class="d-flex flex-row my-3">
//                             <div class="text-warning mb-1 me-2">
//                                 <i class="fa fa-star"></i>
//                                 <i class="fa fa-star"></i>
//                                 <i class="fa fa-star"></i>
//                                 <i class="fa fa-star"></i>
//                                 <i class="fas fa-star-half-alt"></i>
//                                 <span class="ms-1">
//                                     4.5
//                                 </span>
//                             </div>
//                             <span class="text-muted"><i class="fas fa-shopping-basket fa-sm mx-1"></i>15 Left</span>
//                             <span class="text-success ms-2">In stock</span>
//                         </div>

//                         <div class="mb-3">
//                             <span class="h5">${a.price} JOD</span>
//                         </div>

//                         <p>
//                             ${a.description}
//                         </p>

//                         <hr />

//                             <div class="row mb-4">
//                                 <div class="col-md-4 col-6 mb-3">
//                                     <label class="mb-2 d-block">Quantity</label>
//                                     <div class="input-group mb-3" style="width: 170px;">
//                                         <input type="number" name="quantitySelected" class="form-control text-center border border-secondary" placeholder="select quantity" aria-label="Quantity" aria-describedby="button-addon1" />
//                                     </div>
//                                 </div>
//                             </div>
//                             <button type="submit" class="btn btn-primary shadow-0"><i class="me-1 fa fa-shopping-basket"></i> Add to cart</button>
//                     </div>
//                 </main>
//                 `;
//                 }

//             });

//         };
//         detailsCard();
//     }
// }
// myData();

function storeID(id) {
    localStorage.setItem("categoryId", id);
}

function deatailId(id) {
    localStorage.setItem("productId", id);
    location.href = "productsDetails.html";
}

////////////////////////////////
// Method 2
// using categoryId API

let n = (localStorage.getItem("categoryId"));
if (n == null || n == 0) {
    var proApiById = "https://localhost:7229/api/Products";
}
else {
    var proApiById = `https://localhost:7229/api/Products/GetProductByCategoryId/${n}`;
}

async function Try2() {

    const categoriesData = await fetch(categoriesAPI);
    let categoriesJSON = await categoriesData.json();

    const product2Data = await fetch(proApiById);
    let pro2JSON = await product2Data.json();

    let catCon = document.getElementById("categoriesCards");
    let proCon = document.getElementById("productsCards");
    let deCon = document.getElementById("detailsCard");


    if (catCon) {
        function categoriesCard() {
            categoriesJSON.forEach((element) => {
                catCon.innerHTML += `
                    <div class="col-lg-4" >
                        <a onclick = "storeID(${element.categoryId})" href="products.html" style="text-decoration: none;">
                            <div class="single-banner categorycard" >
                                <img src="${element.categoryImage}" alt="" />
                                <div class="inner-text">
                                    <h4>${element.categoryName}</h4>
                                </div>
                            </div>
                        </a>
                    </div>
                `;
            });
        };
        categoriesCard();
    };


    if (proCon) {
        function products2Card() {
            pro2JSON.forEach((t) => {
                var localID = localStorage.getItem("categoryId");
                if (localID == null || t.categoryId == localID) {
                    proCon.innerHTML += `
                    <div class="col-md-4 mb-3">
                        <div class="card" style="margin:10px;">
                            <img class="card-img-top" src="${t.productImage}" alt=" " style="height:300px;">
                            <div class="card-body">
                                <p>${t.productName}</p>
                                <p>${t.price} JOD</p>
                                <p>${t.description}</p>
                            </div>
                            <div class="card-footer">
                                <input type="submit" onclick="deatailId(${t.productId})" value="Details" class="btn" style="color: #283618" />
                            </div>
                        </div>
                    </div>
                `;
                }
            });
        };
        products2Card();
        localStorage.clear();
    };


    if (deCon) {
        function detailsCard() {

            pro2JSON.forEach((a) => {

                if (localStorage.getItem("productId") !== null && a.productId == localStorage.getItem("productId")) {
                    deCon.innerHTML += `
                                        <aside class="col-lg-6">
                        <div class="border rounded-4 mb-3 d-flex justify-content-center">
                            <a data-fslightbox="mygalley" class="rounded-4" target="_blank" data-type="image">
                                <img style="max-width: 100%; max-height: 100vh; margin: auto;" class="rounded-4 fit" src="${a.productImage}" />
                            </a>
                        </div>
                    </aside>
                    <main class="col-lg-6">
                        <div class="ps-lg-3">
                            <h4 class="title text-dark">
                                ${a.productName}
                            </h4>
                            <div class="d-flex flex-row my-3">
                                <div class="text-warning mb-1 me-2">
                                    <i class="fa fa-star"></i>
                                    <i class="fa fa-star"></i>
                                    <i class="fa fa-star"></i>
                                    <i class="fa fa-star"></i>
                                    <i class="fas fa-star-half-alt"></i>
                                    <span class="ms-1">
                                        4.5
                                    </span>
                                </div>
                                <span class="text-muted"><i class="fas fa-shopping-basket fa-sm mx-1"></i>15 Left</span>
                                <span class="text-success ms-2">In stock</span>
                            </div>
    
                            <div class="mb-3">
                                <span class="h5">${a.price} JOD</span>
                                <h1>${a.productId}</h1>
                            </div>
    
                            <p>
                                ${a.description}
                            </p>
    
                            <hr />
    
                                <div class="row mb-4">
                                    <div class="col-md-4 col-6 mb-3">
                                        <label class="mb-2 d-block">Quantity</label>
                                        <div class="input-group mb-3" style="width: 170px;">
                                            <input type="number" name="quantitySelected" class="form-control text-center border border-secondary" placeholder="select quantity" aria-label="Quantity" aria-describedby="button-addon1" />
                                        </div>
                                    </div>
                                </div>
                                <button type="submit" class="btn btn-primary shadow-0"><i class="me-1 fa fa-shopping-basket"></i> Add to cart</button>
                        </div>
                    </main>
                    `;
                }

            });

        };
        detailsCard();
    }
};
Try2();