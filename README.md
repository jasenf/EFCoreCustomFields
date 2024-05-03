# EFCoreCustomFields
## Introduction
Many systems like CRM apps need flexible custom fields that users can tweak. Traditional methods like JSON columns aren't great for performance or type safety. Our library shakes things up by adopting the Entity Attribute Value (EAV) model differently. Instead of one huge table for custom fields, we let each entity have its own, ensuring better performance and scalability.

## Constraint
The only constraint is the uniformity in the type of keys used. Applications utilizing various key types will need a separate class for each key type.

## Features
Dynamic custom fields per entity
Improved performance and type safety
Easy integration with existing EF Core setups

## Getting Started
Clone the repository and include it in your .NET project. Detailed setup instructions will follow in the documentation.

## Contributions
Feel free to fork the repo, push your changes, and open a pull request. We appreciate your input!

## License
This project is available under the MIT License. See the LICENSE file for more details.
