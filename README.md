<!-- 
*** This readme is based on https://github.com/othneildrew/Best-README-Template
*** Check them out, it's a great project!
-->
<a name="readme-top"></a>

<!-- PROJECT SHIELDS -->

![NuGet Version](https://img.shields.io/nuget/v/EFCoreCustomFields?style=for-the-badge)
![NuGet Downloads](https://img.shields.io/nuget/dt/EFCoreCustomFields?style=for-the-badge)


<!-- PROJECT HEADER -->

# EFCoreCustomFields



<!-- TABLE OF CONTENTS -->

<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#introduction">Introduction</a>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
      <ul>
        <li><a href="#prerequisites">Prerequisites</a></li>
        <li><a href="#package-installation">Package Installation</a></li>
        <li><a href="#database-requirements">Database Requirements</a></li>
      </ul>
    </li>
    <li><a href="#usage">Usage</a></li>
    <li><a href="#roadmap">Roadmap</a></li>
    <li><a href="#contributing">Contributing</a></li>
    <li><a href="#license">License</a></li>
    <li><a href="#contact">Contact</a></li>
    <li><a href="#acknowledgments">Acknowledgments</a></li>
  </ol>
</details>

<!-- ABOUT THE PROJECT -->

## Introduction

Many systems like CRM apps need flexible custom fields that users can tweak. Traditional methods like JSON columns aren't great for performance or type safety.  This library allows you to adopt the Entity Attribute Value (EAV) model allowing you to place custom fields in a child table for the entities. Instead of one huge table for custom fields, this library allows each entity have its own, ensuring better performance and scalability.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

## Features

* Dynamic custom fields per entity
* Improved performance and type safety
* Easy integration with existing EF Core setups

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- GETTING STARTED -->

## Getting Started

Please review the following to get started working with this library.

### Prerequisites

This project uses .NET 8.  For ideal compatibility, your project should also target .NET 8 or higher.

This project depends on Microsoft.EntityFrameworkCore version 8.0.4

### Package Installation

Add a reference to the EFCoreCustomFields NuGet package to the project with your data layer.

### Database Requirements

The sample project, and this documentation, is going to assume a relational database using SQL; specifically, SQL Server in our example.

#### Tables

Essentially, each entity to which you would like to add custom fields will have its own ENTITYCustomFields table.  For example, in the sample application, the Customer and Product entities have their own CustomerCustomFields and ProductCustomFields tables.

![Database Tables with Custom Fields](./assets/images/database-tables.png "Database Tables with CustomFields")

You will also have a CustomFields table.  This table will define the custom fields themselves and includes a column to reference the table/entity in which it is used.

Let's look at the Products and ProductCustomFields tables to explain the relationship between an entity, the entity's custom fields table, and the custom fields table itself.

Here's the Products table data:

![Products Table](./assets/images/products-table.png "Products table")

Here's the ProductCustomFields table data:

![ProductCustomFields Table](./assets/images/productcustomfields-table.png "ProductCustomFields table")

Here's the CustomFields table data:

![CustomFields Table](./assets/images/customfields-table.png "CustomFields table")

As you can see, the Products table schema is unchanged.  We are not modifying the existing table.  That said, we do need to modify the Product entity

Each of the entities to which you would like to add custom fields will have a foreign key reference column to the Cus

To work with this library, the EF Core entities to which you would like to add custom fields will need to implement the ICustomFieldEntity interface.



You will also need a custom fields table.

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- CONTRIBUTING -->

## Contributions

Feel free to fork the repo, push your changes, and open a pull request. We appreciate your input!

<p align="right">(<a href="#readme-top">back to top</a>)</p>

<!-- LICENSE -->

## License

This project is available under the MIT License. See the LICENSE file for more details.

<p align="right">(<a href="#readme-top">back to top</a>)</p>
<!-- MARKDOWN LINKS AND IMAGES -->
