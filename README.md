# 🚀 ProSphere

> A powerful platform that connects **creators with investors** — inspired by the spirit of Shark Tank, built with production-grade backend architecture.

---

## 🌌 Overview

**ProSphere** is a full-featured backend system designed to bridge the gap between **innovative creators** and **potential investors**.

Creators can showcase their ideas, while investors explore, connect, and fund promising projects — all within a scalable, secure, and real-time environment.

---

## 🧠 Architecture & Design

Built with **modern backend engineering principles**:

- 🧩 Vertical Slice Architecture
- ⚡ CQRS (Command Query Responsibility Segregation)
- 📨 MediatR (Decoupled request handling)
- 🧼 Clean Code Principles
- 📦 Repository Pattern & Unit of Work
- 🎯 Result Pattern
- ⚙️ Options Pattern

---

## 🛠️ Tech Stack

### ⚙️ Backend
- ASP.NET Core Web API (.NET 10)
- Entity Framework Core
- LINQ

### 🔐 Security
- ASP.NET Identity
- JWT Authentication

### 📡 Real-Time & Communication
- SignalR (Real-time chat & notifications)
- SendGrid (Email service)
- Zoom Integration (Video meetings)

### ☁️ Storage & Infrastructure
- Supabase (Cloud Storage)
- In-Memory Caching
- Hangfire (Background Jobs)

### 🧪 Reliability & Monitoring
- Serilog (Logging)
- Health Checks
- Rate Limiting
- Global Exception Handling

### 🧾 Validation
- FluentValidation

---

## ✨ Core Features

### 🔐 Authentication & Authorization

ProSphere implements a secure and scalable **authentication & authorization system** built on industry best practices.

It ensures that every request is **verified, validated, and properly authorized**.

---

### 🔑 Authentication Features

#### 🔓 Login & Logout
- Secure user login with JWT
- Token-based authentication for stateless APIs
- Logout invalidates user session (token handling)

---

#### 🔄 Refresh Tokens
- Supports token renewal without re-login
- Enhances security and user experience
- Prevents frequent authentication requests

---

#### 📧 Email Confirmation
- Required after registration
- Uses secure token-based verification
- Prevents fake or invalid accounts

---

#### 🔁 Password Recovery & Reset
- Forgot password via email
- Secure reset with token
- Supports resend token functionality

---

### 🛡️ Authorization System

- Role-based access control (RBAC)
- Protected endpoints using `[Authorize]`
- Roles include:
  - 👑 Admin
  - 🛡️ Moderator
  - 💼 Investor
  - 🎨 Creator

---

### 🔐 Security Layers

- JWT Authentication
- ASP.NET Identity integration
- Secure token generation & validation
- Rate limiting to prevent abuse
- Global exception handling

---


---

### 👤 Account Management

ProSphere provides a flexible and powerful **multi-role account management system** that supports different user types and workflows.

---

### 🧩 Role System

Each user belongs to a specific role with distinct permissions:

- 👑 **Admin** → Full system control
- 🛡️ **Moderator** → Handles moderation & verification
- 💼 **Investor** → Explores and funds projects
- 🎨 **Creator** → Builds and manages projects

---

### ⚙️ Core Features

#### ✏️ Profile Management
- Update account information
- Supports file uploads (e.g. profile images)
- Separate handling for creators and investors

---

#### 📊 Account Retrieval
- Get accounts by role:
  - Admins
  - Moderators
  - Investors
  - Creators
- Pagination & filtering support

---

#### 🗑️ Secure Account Deletion

A two-step secure deletion process:

1. 📩 User requests account deletion
2. 🔐 OTP is sent via email
3. ✅ User confirms deletion using OTP
4. 🧹 Account is permanently removed

---

### 🚀 Project Management

At the heart of ProSphere lies a powerful **project lifecycle system** that enables creators to build, evolve, and present their ideas to the world.

---

### ⚙️ Core Capabilities

#### 🆕 Create Projects
- Creators can submit new project ideas
- Rich project data with structured inputs
- Automatically enters moderation pipeline

---

#### ✏️ Update Projects
- Modify existing projects with new information
- Maintain **version control** for updates
- Track changes over time

---

#### 🗑️ Delete Projects
- Secure deletion with ownership validation
- Ensures data integrity and access control

---

### 🧠 Project Versioning

- Each update creates a **new version snapshot**
- Allows moderators to review changes before publishing
- Ensures transparency and prevents misuse

---

### 🔍 Project Insights

- Retrieve full project data
- Access detailed project views
- Separate endpoints for:
  - Basic data
  - Detailed information
  - Update versions

---

### 🛡️ Moderation Workflow

All projects go through a **controlled approval pipeline**:

1. 📤 Creator submits project
2. 🕒 Status → `Pending`
3. 🛡️ Moderator reviews submission
4. ⚖️ Decision:
   - ✅ Accept → Project becomes visible
   - ❌ Reject → Feedback may be provided

---



---

### 🤝 Investor Access System

ProSphere introduces a **controlled access mechanism** that protects creators’ ideas while allowing investors to explore opportunities.

---

### 🔐 Access Flow

1. 💼 Investor requests access to a project
2. 🕒 Request status → `Pending`
3. 🎨 Creator reviews the request
4. ⚖️ Decision:
   - ✅ Accept → Investor gains full visibility
   - ❌ Reject → Access denied

---

### 🎯 Key Features

- Protects sensitive project data
- Ensures only **approved investors** can view full details
- Supports request tracking for both creators and investors

---

### 📊 Access Management

- Creators can:
  - View all incoming access requests
  - Filter by status
  - Accept / reject requests

- Investors can:
  - Track their request status
  - View approved projects

---


---

### 📊 Voting System

A lightweight but powerful **community-driven ranking system** that highlights the most promising projects.

---

### ⚙️ Core Functionality

- Creators can vote on projects
- Each vote contributes to project visibility
- Prevents duplicate voting per user

---

### 📈 Purpose

- Surface high-potential ideas
- Encourage community engagement
- Provide quick insight into project popularity

---

### 🔍 Insights

- Retrieve all voters for a project
- Track engagement levels
- Analyze popularity trends

---

### 💬 Real-Time Chat

ProSphere delivers a seamless **real-time communication system** that connects creators and investors instantly.

Built using **SignalR**, the chat system enables fast, reliable, and interactive conversations.

---

### ⚙️ Core Features

#### 💌 Direct Messaging
- One-to-one messaging between users
- Instant message delivery using WebSockets
- Supports continuous conversations

---

#### 📜 Chat History
- Retrieve full conversation history
- Paginated messages for performance
- Persistent storage for long-term access

---

#### 👥 Contacts System
- Automatically builds user contact lists
- Search contacts by name
- Optimized for quick access to active conversations

---

#### 📎 Media & File Support
- Send attachments (images, files, etc.)
- Integrated with cloud storage (Supabase)
- Secure file handling

---


---

### 🧠 Smart Search

A powerful and flexible **search engine** that allows users to discover relevant people and opportunities across the platform.

---

### 🔍 Search Capabilities

#### 🎨 Creators
- Search by username
- Filter by verification status

---

#### 💼 Investors
- Search by username
- Filter by:
  - Verification status
  - Financial verification
  - Professional verification

---

#### 🚀 Projects
- Search by project name
- Quickly discover relevant ideas

---

### 📜 Search History

- Automatically stores user search activity
- Enables quick access to previous searches
- Improves user experience and navigation

---


---

### 🔔 Notifications System

A real-time **notification engine** that keeps users informed about important events and interactions.

---

### ⚙️ Core Features

#### 📡 Real-Time Notifications
- Instant delivery using SignalR
- Covers key events such as:
  - Access requests
  - Messages
  - Verification updates
  - Reports & moderation actions

---

#### 📬 Notification Management
- Retrieve all notifications (paginated)
- Mark individual notifications as read
- Delete specific notifications

---

#### 🧹 Bulk Operations
- Delete all notifications at once
- Keep inbox clean and organized

---


---

### 📄 CV & Skills Management

A comprehensive system that allows creators to **showcase their expertise** and strengthen their professional profile.

---

### 📂 CV Management

#### 📤 Upload CV
- Upload CV files securely
- Stored using cloud storage (Supabase)

---

#### 📥 Retrieve CV
- Access CV by user ID
- Enables investors to evaluate creators بسهولة

---

#### 🗑️ Delete CV
- Remove CV when needed
- Maintains user control over data

---

### 🧠 Skills Management

#### ➕ Add Skills
- Creators can add multiple skills
- Structured and validated input

---

#### ➖ Remove Skills
- Delete selected skills بسهولة
- Keep profile updated

---

#### 🔍 Skills Discovery
- Search for skills across the platform
- Helps in finding creators by expertise

---

#### 📊 Skills Insights
- Retrieve skills with pagination
- Track skill usage and trends

---

## 🛡️ Advanced Verification System

ProSphere implements a **multi-layered verification system** to ensure trust, credibility, and platform integrity.

This system is designed to validate both **identity** and **investor legitimacy**, creating a safe environment for high-value interactions.

---

### 🧾 Verification Types

#### 🪪 Identity Verification (All Users)
- Users upload official identity documents
- Supports document-based validation (e.g. ID cards, passports)
- Ensures real-user authenticity
- Required for platform trust & interaction

---

#### 💰 Financial Verification (Investors)
- Validates investor financial capability
- Users upload financial proof documents
- Ensures creators interact with **serious investors only**

---

#### 🧑‍💼 Professional Verification (Investors)
- Verifies professional background
- Confirms expertise, experience, or business credibility
- Adds an extra trust layer for high-level collaborations

---

### 📂 Document Handling

- Secure file uploads using **Supabase Cloud Storage**
- Supports multiple document types
- Validation handled via **FluentValidation**
- Files are linked to verification requests and users

---

### 🧠 Verification Workflow

1. 📤 User submits verification request (with documents)
2. 🕒 Status becomes: `Pending`
3. 🛡️ Moderator reviews submission
4. ✅ Approved → Status: `Accepted`
5. ❌ Rejected → Status: `Rejected` (with reason)

---

### ⚖️ Moderator Control Panel

Moderators have full control over the verification lifecycle:

- Review submitted documents
- Accept or reject verification requests
- Provide rejection reasons
- Manage different verification types independently

---

### 🔍 Verification Queries

Moderators can retrieve:

- 📋 All identity verification requests
- 💰 Financial verification requests
- 🧑‍💼 Professional verification requests
- 🔎 Filter by status (Pending / Accepted / Rejected)
- 📄 Detailed view for each submission

---


## 🚨 Reporting System

ProSphere includes a **robust reporting and moderation system** designed to maintain a safe, respectful, and trustworthy environment.

Users can report both **projects** and **other users**, while moderators review and take appropriate actions.

---

### 📌 What Can Be Reported?

#### 📁 Projects
- Inappropriate or harmful content
- Fraudulent or misleading ideas
- Violation of platform policies

#### 👤 Users
- Spam or abusive behavior
- Fake accounts
- Harassment or misconduct

---

### 🧾 Report Reasons

- Predefined list of report reasons (retrievable via API)
- Ensures consistent and structured reporting
- Helps moderators make faster, informed decisions

---

### 🧠 Reporting Workflow

1. 🚩 User submits a report (project or user)
2. 🕒 Report status becomes: `Pending`
3. 🛡️ Moderator reviews the report
4. ⚖️ Decision:
   - ✅ Accept → Action is applied
   - ❌ Reject → Report dismissed

---

### ⚖️ Moderator Actions

#### ✅ Accept Report on Project
- Project may be flagged or restricted
- Further actions can be taken by system admins

#### ✅ Accept Report on User
- Temporary ban (configurable number of days)
- Helps enforce platform discipline

#### ❌ Reject Report
- Report is dismissed
- No action taken

---

### 🔍 Moderation Capabilities

Moderators can:

- View all reports (users & projects)
- Filter reports with pagination
- Access detailed report data
- Take action (accept / reject)
- Apply penalties on users

---


### 👥 Admin & Employee Management

ProSphere provides a structured **administrative hierarchy** to efficiently manage platform operations, moderation workflows, and internal responsibilities.

---

### 🏢 Roles & Responsibilities

#### 👑 Admin
- Highest level of control
- Create and manage other admins
- Oversee system-wide operations
- Maintain platform governance

---

#### 🛡️ Moderator
- Handles user and project moderation
- Reviews:
  - Verification requests
  - Reports
  - Project approvals
- Ensures platform compliance and quality

---

#### 🧑‍💻 Employee
- Operational support role
- Works under moderators
- Can be assigned specific moderation-related tasks

---

### ⚙️ Core Functionalities

#### 🧩 Admin Management
- Create new admins
- Delete existing admins
- Maintain administrative structure

---

#### 👨‍💼 Employee Management
- Create employees
- Retrieve employees with filtering (name, country)
- Delete employees

---

#### 🔗 Assignment System
- Assign employees to moderators
- Enables workload distribution
- Improves moderation efficiency and scalability

---

### 🧠 Workflow Example

1. 👑 Admin creates employees
2. 🔗 Employees are assigned to moderators
3. 🛡️ Moderators manage tasks with assigned support
4. ⚖️ Moderation actions are executed efficiently

---

### 🎥 Video Meetings

ProSphere enables seamless **real-time video communication** between creators and investors, transforming connections into meaningful discussions.

Built with **Zoom integration**, this feature bridges the gap between ideas and investment through direct interaction.

---

### 🎯 Purpose

- Enable **face-to-face communication** between creators and investors
- Facilitate pitch discussions and negotiations
- Strengthen trust through real-time interaction

---

### ⚙️ Core Functionality

#### 🎬 Meeting Generation
- Dynamically generate video meeting sessions
- Secure meeting data creation via backend
- Supports one-to-one communication

---

#### 🔗 User-Based Sessions
- Meetings are created between:
  - 🎨 Creator
  - 💼 Investor
- Ensures private and focused discussions

---

#### ☁️ Zoom Integration
- Leverages Zoom API for:
  - Meeting creation
  - Session metadata (link, ID, etc.)
- Scalable and reliable video infrastructure

---

### 🧠 Workflow

1. 👤 User initiates a meeting request
2. ⚙️ Backend generates meeting via Zoom
3. 🔗 Meeting details are returned (link / session data)
4. 🎥 Both users join and start communication

---

### 💬 Social Media Integration

ProSphere allows users to connect their **social media accounts** to enhance their profile authenticity and professional presence.

This feature helps build **trust, transparency, and credibility** between creators and investors.

---

### 🎯 Purpose

- Showcase external presence and reputation
- Provide additional context about users
- Increase confidence during collaborations

---

### ⚙️ Core Functionality

#### 🔗 Link Social Accounts
- Users can attach multiple social media profiles
- Supports platforms such as:
  - LinkedIn
  - Twitter (X)
  - Facebook
  - GitHub
  - Portfolio websites

---

#### 👀 View Social Profiles
- Retrieve all linked social accounts for a user
- Easily accessible from user profile
- Helps investors evaluate creators beyond the platform

---

#### ✏️ Update Social Links
- Add new accounts or update existing ones
- Flexible structure to support multiple platforms

---

### 🧠 Use Case

- 🎨 Creator shares portfolio and GitHub  
- 💼 Investor reviews LinkedIn and experience  
- 🤝 Both sides gain confidence before engaging  

---



## 🔄 Background Processing

- Email sending (SendGrid)
- Notifications
- Scheduled jobs using **Hangfire**

---

## ⚡ Performance & Reliability

- Caching (In-Memory)
- Rate Limiting
- Health Monitoring
- Structured Logging with Serilog
