# File Conductor
 <!-- Replace with your CI link when ready -->


A cross-platform virtual filesystem in C# that uses content-addressable storage for powerful data deduplication. File Conductor allows you to mount "projects" as virtual folders, saving significant disk space by ensuring that any given file is only ever stored once.

Core Concepts
File Conductor is built on a few key architectural ideas to provide an efficient and flexible way to manage large sets of files, especially across multiple projects that share common assets (e.g., game development, video editing, personal document archives).

Content-Addressable Storage: Files are not identified by their name or location, but by a unique hash of their content. This is the core of the deduplication engine—if two files have the same content, they will have the same hash and only be stored once.

Central Asset Store: All unique file data is stored in a single, expandable virtual disk file (data.vhdx). This container holds the deduplicated data blocks.

Project Manifests: Each project is defined by a simple JSON file. This manifest acts as a "map," defining the project's directory structure and pointing each virtual file to its corresponding content hash in the central store.

Virtual Filesystem Driver: Using Dokan (on Windows) and FUSE (on Linux/macOS), File Conductor presents a project manifest as a live, explorable folder or drive that any application can interact with.

⚠️ Current Status: Work in Progress
This project is currently in the early stages of development and is not yet ready for production use. The core architecture is being built, and the feature set is incomplete.

Project Roadmap
The following is a high-level plan for the development of File Conductor:

[x] Define core architecture and data models.

[ ] VHDX Management: Implement the core service for creating, reading, and writing to the central data.vhdx asset store using DiscUtils.

[ ] Database Service: Implement the LiteDB-based database manager for tracking file manifests and content hash reference counts.

[ ] Dokan (Windows) Driver: Implement the IDokanOperations2 interface to mount projects as virtual drives on Windows.

[ ] FUSE (Linux) Driver: Implement the IFuseOperations interface to mount projects as virtual folders on Linux.

[ ] Command-Line Interface: Build the main console application for creating, managing, and mounting projects.

[ ] Garbage Collection: Implement a utility to scan the database for unreferenced hashes and reclaim space in the VHDX.

Technology Stack
Framework: .NET 8

Language: C#

Virtual Filesystem (Windows): DokanNet

Virtual Filesystem (Linux): FuseDotNet

Virtual Disk Management: DiscUtils

Internal Filesystem Abstraction: Zio

Database: LiteDB

Getting Started
Note: Detailed build and usage instructions will be added once the project reaches a more stable state.

Clone the repository: git clone https://github.com/JAAYapps/FileConductor.git

Open the solution (FileConductor.sln) in your preferred .NET IDE (e.g., Visual Studio, JetBrains Rider).

Build the solution to restore NuGet packages.

Contributing
Contributions are welcome! If you have ideas, suggestions, or find a bug, please feel free to open an issue or submit a pull request.

License
This project is licensed under the MIT License. See the LICENSE file for details.
