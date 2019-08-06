# LibraryManager

![Screen upon startup](https://i.imgur.com/DcIdyZC.png)

Library Manager is a client database program designed to manage the issuance of books for a school library. The application includes features such as managing history, adding and removing items, and a system for reserving books for varying durations. The project as a whole is a component of a submission into the Future Business Leaders of America's *Coding & Programming* competition, the specific requirements for which were listed [on their website (archived)](https://web.archive.org/web/20180217192136im_/http://www.fbla-pbl.org:80/competitive-event/coding-programming/). A functioning copy is [available here](https://github.com/jazevedo620/LibraryManager/releases/latest) (runnable on the .NET runtime).

## 🏛 Underlying Architecture

As part of the design guidelines, Library Manager is a Client database programmed designed to be connected to a centralized database server. Instead of locally serving the database, the demo provided under Releases uses a sample dataset in order to showcase functionality.

Internally, Library Manager takes advantage of Microsoft's flagship .NET UI framework, Windows Presentation Foundation, or WPF. WPF works through two components: the design is programmed in **XAML**, or Extensible Application Markup Language (*different from XML*), while the behavior is programmed in **C#**. This separation of *design* and *behavior* is key to the framework's architecture, as it allows for the adoption of the Model-View-ViewModel design paradigm as outlined below.

<img src="https://i.imgur.com/k2jowmW.png" alt="mvvm" width="345" height="300" border="10" />

## 📚 Documentation

Extensible documentation was developed via LaTeX in order to explain each feature of the program, and is available as a [PDF document](https://drive.google.com/open?id=1rzvxD1XwVbWNX5nE0kkMXI031g4EwGW_).

<a href="https://drive.google.com/open?id=1rzvxD1XwVbWNX5nE0kkMXI031g4EwGW_" target="_blank"><img src="https://i.imgur.com/NC5QYGQ.png" alt="documentation" width="277" height="300" border="10" /></a>

## 📐 Design Process

A brief slideshow discussing the development of the project has been created, and is available below.
<a href="https://docs.google.com/presentation/d/e/2PACX-1vTGNy98lkIgXEggQ311Q4-lEVoktZXNi6ZJbzeMykYx1stRzgmh1ap6npJRz_zBeCNXJ6MNUN8nVQ-w/pub?start=false&loop=false&delayms=3000" target="_blank"><img src="https://i.imgur.com/7RM6kRB.png" alt="slideshow" width="400" height="224" border="10" /></a>

## 🔗 Open Source External Packages

* [ControlsEx v3.0.2.4](https://github.com/ControlzEx/ControlzEx) by Jan Karger, Bastian Schmidt, and James Willock: provides useful UI controls and styling with which to build the application frontend
* [iTextSharp v5.5.13](https://github.com/itext/itextsharp) by Bruno Lowagie, Paulo Soares, et al: Open-source framework for PDF creation in C#; used for generating reports
* [MahApps.Metro v1.5.0](https://mahapps.com/) by Jan Karger, Dennis Daume, Brenden Forster, Paul Jenkins, Jake Ginnivan, and Alex Mitchel: provides UI styling for a majority of the elements in the application front-end in order to conform to Microsoft's Metro design paradigm within WPF applications
