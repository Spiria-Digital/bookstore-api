# Bookstore API

This is a simple API for a bookstore. It allows you to create, read, update, and delete books.
The whole API is build to test a candidate's skills in building a RESTful API using .NET Core.

## Getting Started
The API is built using .NET Core 9.0.202. You can run the API by running the following command in the root directory of the project:

```bash
dotnet run --project Bookstore.API/Bookstore.API.csproj
```

The API will be available at `http://localhost:5260`. You can test the API using Postman or any other API testing tool but Swagger is also available at `http://localhost:5260/index.html`.

## Technical Assessment
Assume that you are building a RESTful API for a bookstore. The database is in-memory and you can use the `Book` model provided in the `Bookstore.API` project. The project manager has provided you with the following high-level requirements (safe to assume that they have not yet been implemented but review the existing code to make sure you don't duplicate existing functionality):

- Only authorized users can create, modify, view or delete orders.
- An authorized user should only be able to modify, delete or view their own orders.
- Anyone can view a list of books we have in store but this insecure endpoint should not list the price nor the quantity of the books.
- An authorized user can view the price and quantity of the books in store.
- Only admin users can create, modify, or delete books in store.
- Only admins can view publisher information in the database.

### The project manager has also provided you with the following user stories:

- As a user, I want to be able to create an order so that I can buy books.
- As a user, I want to be able to view my orders so that I can see what I have bought.
- As a user, I want to be able to modify my orders so that I can change the quantity of books I have bought.
- As a user, I want to be able to delete my orders so that I can cancel my order if it has not yet been shipped.
- As a user, I want to be able to view the books in store so that I can see what books are available.
- As an admin, I want to be able to create a book so that I can add new books to the store.
- As an admin, I want to be able to modify a book so that I can change the price or quantity of a book.
- A book that has orders related to it should not be deleted

### The project manager has also provided you with the following acceptance criteria:

- An order should have a unique identifier, a user identifier.
- An order can have at minimum one book but for each book, there will be a quantity related.
- Once an order has been created and shipped, it should not be modified or deleted.
- Once an order is placed, the quantity available for each book should be reduced.
- An order should have a total price which is the sum of the price of each book multiplied by the quantity plus a 10% sales tax and an additional 2.99% for shipping.
- Do not allow orders for books that are out of stock and don't sale more books than are available.
- An order should have a status that can be one of the following: `Pending`, `Shipped`, `Delivered`, `Cancelled`.
- Be mindful of personal information which could be sensitive. In this case, what author's information is considered sensitive and should be omitted from the book details? Checkout [PIPEDA](https://www.priv.gc.ca/en/privacy-topics/privacy-laws-in-canada/the-personal-information-protection-and-electronic-documents-act-pipeda/p_principle/) for more information.

### The project manager has also provided you with the following additional information:

- The project manager tells you to use the provided models and to modify them where necessary.
- The project manager tells you to continue using the in-memory database provided in the project.
- Use the controllers provided in the project and modify them where necessary. Add new controllers if needed.

### The project manager has also provided you with the following constraints:

- The project manager is gone on vacation and will be back in 2 weeks. During this period you are expected to complete the user stories and acceptance criteria provided.
- The project manager has told you that you can ask for help from the team if you need it.

## Submission
Fork this repository to your own GitHub account and clone it to your local machine.
Please submit your solution as a pull request to this repository. The pull request should include the following:

- A detailed description of the changes you made.
- A detailed description of any assumptions you made where the requirements were unclear.
- A detailed description of any issues you encountered and how you resolved them.
- A detailed description of any improvements you made to the existing code.
- A detailed description of any new features you added.
- A detailed description of any new tests you added.
- A detailed description of how to test your solution.

> HAPPY CODING! 🚀
