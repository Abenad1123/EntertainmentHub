<p align="center">
  <img src="\EntertainmentHub\Assets\Images\full_logo.png" alt="Entertainment Cafe & PC Management System Logo" width="500">
</p>

# Entertainment Cafe & PC Management System

A centralized software solution designed to streamline the operations of modern gaming and internet cafes. This system seamlessly integrates hardware monitoring, user account management, automated billing, and point-of-sale (POS) features into a single, cohesive platform—ensuring efficient resource allocation, precise financial tracking, and an optimal customer experience.

<br><br><br><br>

## Key Features & Core Requirements

### I. Infrastructure & Monitoring
*   **Terminal Status Monitoring:** Real-time visibility into computer terminal statuses (`Available`, `In Use`, `Maintenance`, `Offline`).
*   **Tier-Based Pricing:** Flexible support for multiple computer tiers (Basic, Regular, VIP) with custom hourly rates and pricing configurations.
*   **Centralized Floor Dashboard:** A main control center displaying terminal status, active users, session duration, and remaining balance/time.
*   **Remote Administration:** Power actions (Restart, Shutdown, Lock Screen) executable remotely by authorized staff.
*   **Idle Activity Detection:** Automatic monitoring for inactive terminals with admin alerts for prolonged inactivity.

### II. User & Session Management
*   **Account Diversity:** Account creation and tiered management for Temporary, Regular, and VIP customers.
*   **Session Tracking & Automated Billing:** Automatic logging of login/logout times, active duration tracking, and dynamic cost calculation.
*   **Automatic Session Termination:** Hard stop mechanisms that instantly end a user's session when their prepaid balance reaches zero.

### III. Financial & POS Operations
*   **Hybrid Payment Processing:** Frictionless transaction processing utilizing both digital account balances and cash.
*   **Digital Receipts:** Electronic generation of receipts for top-ups, active sessions, product purchases, and processing refunds.
*   **Inventory & Product Sales:** Built-in tracking of stock levels and sales workflows for food, drinks, or retail items.
*   **Wallet Management:** Comprehensive transactional history logging for all top-ups, account debits, and adjustments.

### IV. Administration & Analytics
*   **Separated Revenue Reporting:** Financial analytics that cleanly split computer runtime revenue from POS product sales.
*   **Usage Analytics:** Concrete reporting on peak usage hours, overall computer utilization rates, and highly popular terminals.
*   **Role-Based Access Control (RBAC):** Tailored system permissions restricted by role (`Admin`, `Manager`, `Cashier`).
*   **Audit Logging & Master Config:** Secure audit trails capturing employee activity logs alongside global system rules (discounts, taxes, rates).

<br><br><br><br>

## Built With

This desktop application architecture relies on the Windows Forms environment powered by Visual Basic .NET to handle hardware management client operations and real-time terminal synchronization.

*   **Language:** Visual Basic (VB.NET)
*   **UI Framework:** Windows Forms (WinForms)
*   **Core Platform:** .NET Framework 4.7.2
*   **Database Engine:** MySQL
*   **Connectivity:** `MySql.Data` ADO.NET Client Driver

<br><br><br><br>

### Setup & Local Installation

1. **Clone the Repository:**
```bash
   git clone https://github.com/Abenad1123/EntertainmentHub.git
