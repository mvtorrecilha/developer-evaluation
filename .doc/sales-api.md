[Back to README](../README.md)

### Sales

#### GET /sales
- Description: Retrieve a list of all sales
- Query Parameters:
  - `_page` (optional): Page number for pagination (default: 1)
  - `_size` (optional): Number of items per page (default: 10)
  - `_order` (optional): Ordering of results (e.g., "id desc, userId asc")
- Response: 
  ```json
  {
    "data": [
    {
      "id": "uuid",
      "customerId": "uuid",
      "customerName": "string",
      "branchId": "uuid",
      "branchName": "string",
      "saleDate": "string (date)",
      "totalSaleAmount": "number",
      "isCanceled": "boolean"
    }
   ]
  }
  ```

#### POST /sales
- Description: Add a new sale
- Request Body:
  ```json
  {
    "userId": "integer",
    "date": "string (date)",
    "products": [
      {
        "productId": "integer",
        "quantity": "integer"
      }
    ]
  }
  {
	  "customerId": "uuid",
	  "customerName": "string",
	  "branchId": "uuid",
	  "branchName": "string",
	  "cartItems": [
		{
		  "productId": "uuid",
		  "productName": "string",
		  "quantity": "integer",
		  "unitPrice": "number"
		}
	  ]
  }
  ```
- Response: 
  ```json
  {
	"id": "uuid",
	"userId": "uuid",
	"saleDate": "2025-08-07T16:59:07.147Z",
	"saleItems": [
	  {
		"productId": "uuid",
		"quantity": "integer",
		"unitPrice": "number",
		"discount": "number",
		"totalAmount": "number"
	  }
	],
	"totalSaleAmount": "number",
	"totalSaleDiscount": "number",
	"branch": "string",
	"isCanceled": "boolean" 
  }
  ```

#### GET /sales/{id}
- Description: Retrieve a specific sale by ID
- Path Parameters:
  - `id`: Sale ID
- Response: 
  ```json
  {
    "id": "uuid",
    "customerId": "uuid",
    "customerName": "string",
    "branchId": "uuid",
    "branchName": "string",
    "saleDate": "string (date)",
    "saleItems": [
      {
        "productId": "uuid",
        "productName": "string",
        "quantity": "integer",
        "unitPrice": "number"
      }
    ],
    "totalSaleAmount": "number",
    "isCanceled": "boolean"
  }
  ```

#### PUT /sales/{id}
- Description: Update a specific sale
- Path Parameters:
  - `id`: Sale ID
- Request Body:
  ```json
  {
	  "id": "uuid",
	  "customerId": "uuid",
	  "customerName": "string",
	  "branchId": "uuid",
	  "branchName": "string",
	  "cartItems": [
		{
		  "productId": "uuid",
		  "productName": "string",
		  "quantity": "integer",
          "unitPrice": "number"
		}
	  ]
  }
  ```
- Response: 
  ```json
	  {
		"id": "uuid",
		"userId": "uuid",
		"saleDate": "string (date)",
		"saleItems": [
		  {
			"productId": "uuid",
			"quantity": "integer",
			"unitPrice": "number",
			"discount": "number",
			"totalAmount": "number"
		  }
		],
		"totalSaleAmount": "number",
		"totalSaleDiscount": "number",
		"branch": "string",
		"isCanceled": "boolean"
	  }
  ```

#### DELETE /sales/{id}
- Description: Delete a specific sale
- Path Parameters:
  - `id`: Sale ID
- Response: 
  ```json
  {
    "message": "string"
  }
  ```
  
[Back to README](../README.md)

### Sales

#### GET /sales
- Description: Retrieve a list of all sales
- Query Parameters:
  - `_page` (optional): Page number for pagination (default: 1)
  - `_size` (optional): Number of items per page (default: 10)
  - `_order` (optional): Ordering of results (e.g., "id desc, userId asc")
- Response: 
  ```json
  {
    "data": [
		{
		  "id": "uuid",
		  "customerId": "uuid,
		  "customerName": "string",
		  "branchId": "uuid",
		  "branchName": "string",
		  "saleDate": "string (date),
		  "saleItems": [
			{
			  "productId": "uuid",
			  "productName": "string",
			  "quantity": "integer",
			  "unitPrice": "number"
			}
		  ],
		  "totalSaleAmount": "number",
		  "isCanceled": "boolean"
		}
	]
  }
  ```

#### POST /sales
- Description: Add a new sale
- Request Body:
  ```json
  {
    "userId": "integer",
    "date": "string (date)",
    "products": [
      {
        "productId": "integer",
        "quantity": "integer"
      }
    ]
  }
  {
	  "customerId": "uuid",
	  "customerName": "string",
	  "branchId": "uuid",
	  "branchName": "string",
	  "cartItems": [
		{
		  "productId": "uuid",
		  "productName": "string",
		  "quantity": "integer",
		  "unitPrice": "number"
		}
	  ]
  }
  ```
- Response: 
  ```json
  {
	"id": "uuid",
	"userId": "uuid",
	"saleDate": "2025-08-07T16:59:07.147Z",
	"saleItems": [
	  {
		"productId": "uuid",
		"quantity": "integer",
		"unitPrice": "number",
		"discount": "number",
		"totalAmount": "number"
	  }
	],
	"totalSaleAmount": "number",
	"totalSaleDiscount": "number",
	"branch": "string",
	"isCanceled": "boolean"  
  }
  ```

#### GET /sales/{id}
- Description: Retrieve a specific sale by ID
- Path Parameters:
  - `id`: Sale ID
- Response: 
  ```json
  {
    "id": "uuid",
    "customerId": "uuid",
    "customerName": "string",
    "branchId": "uuid",
    "branchName": "string",
    "saleDate": "string (date)",
    "saleItems": [
      {
        "productId": "uuid",
        "productName": "string",
        "quantity": "integer",
        "unitPrice": "number"
      }
    ],
    "totalSaleAmount": "number",
    "isCanceled": "boolean"
  }
  ```

#### PUT /sales/{id}
- Description: Update a specific sale
- Path Parameters:
  - `id`: Sale ID
- Request Body:
  ```json
  {
	  "id": "uuid",
	  "customerId": "uuid",
	  "customerName": "string",
	  "branchId": "uuid",
	  "branchName": "string",
	  "cartItems": [
		{
		  "productId": "uuid",
		  "productName": "string",
		  "quantity": "integer",
          "unitPrice": "number"
		}
	  ]
  }
  ```
- Response: 
  ```json
  {
	"id": "uuid",
	"userId": "uuid",
	"saleDate": "string (date)",
	"saleItems": [
	  {
		"productId": "uuid",
		"quantity": "integer",
		"unitPrice": "number",
		"discount": "number",
		"totalAmount": "number"
	  }
	],
	"totalSaleAmount": "number",
	"totalSaleDiscount": "number",
	"branch": "string",
	"isCanceled": "boolean"
  }
  ```

#### DELETE /sales/{id}
- Description: Delete a specific sale
- Path Parameters:
  - `id`: Sale ID
- Response: 
  ```json
  {
    "message": "string"
  }
  ```
  
#### Cancel /sales/{id}/cancel
- Description: Cancel a specific sale
- Path Parameters:
  - `id`: Sale ID
- Response: 
  ```json
  {
    "message": "string"
  }
  ```

#### Cancel /sales/{id}/items/{itemId]cancel
- Description: Cancel a specific sale
- Path Parameters:
  - `id`: Sale ID
  - `itemId`: Item ID
- Response: 
  ```json
  {
    "message": "string"
  }
  ```

<br>
<div style="display: flex; justify-content: space-between;">
	<a href="./carts-api.md">Previous: Carts API</a>
  <a href="./users-api.md">Next: Users API</a>
</div>
  ```


<br>
<div style="display: flex; justify-content: space-between;">
	<a href="./carts-api.md">Previous: Carts API</a>
  <a href="./users-api.md">Next: Users API</a>
</div>