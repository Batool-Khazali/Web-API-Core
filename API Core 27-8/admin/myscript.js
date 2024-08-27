function toasty() {
    var x = document.getElementById("snackbar");
    x.className = "show";
    setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
}

// //////////////////////////////////////////////////////////////////////////////////////////////////
// //////////////////////////////////////////////////////////////////////////////////////////////////
// //////////////////////////////////////////////////////////////////////////////////////////////////


if (document.getElementById("catTable")) {
    const allCatAPI = "https://localhost:7192/api/Categories";

    async function allCAt() {
        const response = await fetch(allCatAPI);
        let data = await response.json();

        var catTable = document.getElementById("catTable");

        function fillCatTable() {
            data.forEach(element => {
                catTable.innerHTML +=
                    `
                                <tr>
                        <td>
                        ${element.categoryId}
                        </td>
                        <td>
                        ${element.categoryName}
                        </td>
                        <td>
                            <img src="../images/${element.categoryImage}" width="80" height="80" alt="Alternate Text" />
                        </td>
                        <td>
                            <a href="editCategory.html" class="btn" onclick="localCatId(${element.categoryId})">Edit</a> |
                            <a href="detailsCategory.html" class="btn" onclick="localCatId(${element.categoryId})">Details</a> |
                            <a href="deleteCategory.html" class="btn" onclick="localCatId(${element.categoryId})">Delete</a>
                        </td>
                    </tr>
            `;
            });
        }
        fillCatTable();
    }
    allCAt();
}



function localCatId(id) {
    localStorage.setItem("categoryId", id);
}



if (document.getElementById("addCategory")) {

    const form1 = document.getElementById("addNewCategory");

    async function sendData() {

        const formData = new FormData(form1);

        const response = await fetch("https://localhost:7192/api/Categories/add", {
            method: "POST",
            body: formData,
        });
    }

    form1.addEventListener("submit", (event) => {
        event.preventDefault();
        sendData();
        toasty();
    });
}



if (document.getElementById("editCategory")) {

    var x = localStorage.getItem("categoryId")
    const url = `https://localhost:7192/api/Categories/${x}`;

    var editform = document.getElementById("editCategoryInfo");

    async function updateCategory() {

        var formData = new FormData(editform);

        var response = await fetch(url, {
            method: "PUT",
            body: formData
        })
    }

    editform.addEventListener("submit", (event) => {
        event.preventDefault();
        updateCategory();
        toasty();
    });
}


// //////////////////////////////////////////////////////////////////////////////////////////////////
// //////////////////////////////////////////////////////////////////////////////////////////////////
// //////////////////////////////////////////////////////////////////////////////////////////////////


if (document.getElementById("productTable")) {
    const allProAPI = "https://localhost:7192/api/Products"

    async function allPro() {
        const response = await fetch(allProAPI);
        let data = await response.json();

        var proTable = document.getElementById("productTableBody");

        function fillProTable() {
            data.forEach(element => { 

                proTable.innerHTML += 
                `
                    <tr>
                        <td>${element.productId}</td>
                        <td>${element.productName}</td>
                        <td>${element.price}</td>
                        <td>${element.description}</td>
                        <td><img src="../images/${element.productImage}" width="80" height="80" alt="Alternate Text" /></td>
                        <td>${element.categoryId}</td>
                        <td><button class="btn btn-sm dropdown-toggle more-horizontal" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                            <span class="text-muted sr-only">Action</span>
                        </button>
                        <div class="dropdown-menu dropdown-menu-right">
                            <a class="dropdown-item" href="#" onclick="localProID(${element.productId})">Details</a>
                            <a class="dropdown-item" href="editProduct.html" onclick="localProID(${element.productId})">Edit</a>
                            <a class="dropdown-item" href="#" onclick="localProID(${element.productId})">Delete</a>
                        </div>
                        </td>
                    </tr>
                `;
            })
        }
        fillProTable();
    };
    allPro();
}

function localProID(id)
{
    localStorage.setItem("productId", id);
}

if (document.getElementById("categoryNameDropDown"))
{
    async function getCategoryName() {
        const dropDown = document.getElementById("categoryNameDropDown");
        let url = "https://localhost:7192/api/Categories";
        let request = await fetch(url);
        let data = await request.json();
      
        data.forEach((select) => {
          dropDown.innerHTML += `
          <option value="${select.categoryId}">${select.categoryName}</option>
        `;
        });
      }
      
      getCategoryName();
}

if (document.getElementById("addProduct"))
{

    const addProAPI = "https://localhost:7192/api/Products";

    const addProForm = document.getElementById("addProductForm");

    async function addProduct()
    {
        const formData = new FormData(addProForm);

        const respone = await fetch (addProAPI , 
            {
                method: "POST",
                body: formData,
            }
        )
        toasty();
    };

    addProForm.addEventListener("submit", (event) => {
        event.preventDefault();
        addProduct();
        toasty();
    });

}

if (document.getElementById("editProduct"))
{
    let x = localStorage.getItem("productId");
    const proByIdAPI =  `https://localhost:7192/api/Products/${x}`;

    const editProForm = document.getElementById("editProductForm");

    async function editProduct()
    {
        const formData = new FormData(editProForm);

        await fetch (proByIdAPI, {
            method: "PUT",
            body: formData,
        }) 
        
    }
    
    editProForm.addEventListener("submit", (event) => {
        event.preventDefault();
        editProduct();
        toasty();
    });
}



