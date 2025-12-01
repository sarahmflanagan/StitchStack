StitchStack
A personal sewing project management application for organizing patterns, fabrics, and tracking sewing projects.
Overview
StitchStack helps me keep track of my sewing materials and projects in one place. I use it to catalog my pattern collection, manage my fabric stash, and plan future sewing projects by combining patterns with fabrics.
Features

Pattern Management: Organize and categorize my sewing pattern collection
Fabric Inventory: Track fabrics by type, color, length, and material
Project Planning: Create sewing projects by pairing patterns with fabrics
Project Tracking: Monitor project status from planning through completion

Tech Stack

Backend: .NET Core / ASP.NET Core
Database: Entity Framework Core with MySQL Database
Language: C#

Data Models
Fabric

Name, Color, Material, Length
Tracks available fabric in my stash

Pattern

Name, Type, Size, Brand
Catalogs my sewing pattern collection

Project

Name, Status, Created Date
Optional references to Fabric and Pattern
Tracks projects from planning to completion

Development Status
This is a personal project currently in active development.
Future Enhancements

Web API endpoints
Search and filtering capabilities
Project completion tracking
Fabric requirement calculations
Photo uploads for patterns and fabrics
Tags and categories
