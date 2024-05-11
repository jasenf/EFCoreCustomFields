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
