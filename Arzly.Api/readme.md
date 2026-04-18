## 📁 Recommended Project Structure

```
📦 Arzly/
├── 📂 Arzly.Shared/               # 🔹 Class Library (Contracts & Cross-Cutting)
│   ├── 📂 Dtos/                   # Request/Response DTOs (shared with API & future admin UI)
│   ├── 📂 Enums/                  # ListingStatus, ChatRole, NotificationSource, ThemeMode, etc.
│   ├── 📂 Exceptions/             # Custom exceptions (NotFoundException, ValidationException)
│   ├── 📂 Constants/              # Roles, Policies, CacheKeys, ErrorCodes
│   ├── 📂 Interfaces/             # IImageStorage, IDateTimeProvider, ICurrentUserService
│   └── 📂 Extensions/             # IQueryable helpers, string sanitizers, enum mappers
│
├── 📂 Arzly.API/                  # 🔹 ASP.NET Core Web API (Main Project)
│   ├── 📂 Controllers/
│   │   ├── 📂 Auth/               # Firebase exchange, email/password login, token refresh
│   │   ├── 📂 Listings/           # Public browse, user CRUD, category endpoints
│   │   ├── 📂 Chat/               # REST endpoints + SignalR Hub
│   │   ├── 📂 Profile/            # Preferences, delivery locations, saved posts, search history
│   │   ├── 📂 Notifications/      # User inbox + admin broadcast triggers
│   │   └── 📂 Admin/              # [Authorize(Roles="Admin")] moderation, stats, broadcast
│   ├── 📂 Hubs/                   # ChatHub.cs (SignalR)
│   ├── 📂 Services/               # Business logic, orchestration, validation
│   ├── 📂 Repositories/           # EF Core implementations (data access only)
│   ├── 📂 Infrastructure/
│   │   ├── 📂 Data/               # AppDbContext, Migrations, SeedData, UnitOfWork
│   │   ├── 📂 Identity/           # AppUser, RoleSeeders, JWT config, Firebase verifier
│   │   └── 📂 Storage/            # LocalImageStorage, S3ImageStorage (implements IImageStorage)
│   ├── 📂 Middleware/             # Global error handler, CORS, rate limiting
│   ├── 📂 Mappings/               # Mapster/AutoMapper profiles (DTO ↔ Entity)
│   ├── 📂 Filters/                # ModelState validation, audit logging
│   └── Program.cs                 # DI, routing, middleware pipeline
```

---

## 🔍 What Goes Where & Why

| Layer | Purpose | Keeps You Safe From |
|-------|---------|---------------------|
| `Arzly.Shared` | **Contracts only**: DTOs, enums, interfaces, constants, exceptions | Duplication if you later add Blazor admin, mobile BFF, or microservices |
| `API/Controllers` | **Routing & HTTP**: Validate input, call services, return DTOs | Business logic bleeding into endpoints |
| `API/Services` | **Use Cases & Orchestration**: Coordinate repos, enforce rules, trigger side-effects | Repos handling transactions or cross-aggregate logic |
| `API/Repositories` | **Data Access Only**: EF Core queries, CRUD, no business rules | N+1 queries, mixed concerns, hard-to-test controllers |
| `API/Hubs` | **Real-time**: SignalR endpoints, reuse same services/repos | Duplicate chat logic between REST & WebSockets |
| `API/Admin/` | **Role-gated**: Clean separation, share infra with user routes | Role spaghetti in `[Authorize]` attributes |



## 🔑 Critical Implementation Notes

### 🔐 Auth Flow (Mobile ↔ Backend)
1. Flutter → Firebase Auth → gets `firebaseIdToken`
2. `POST /api/auth/firebase-login { firebaseToken }` → Backend verifies via Firebase Admin SDK
3. If valid, backend finds/creates `AppUser`, assigns `User` role, issues JWT
4. Flutter stores JWT → attaches to all API calls
5. **Admin/Support** bypass Firebase → use `POST /api/auth/login` with email/password → direct JWT issuance

### 💬 Chat + SignalR Design
- **Hub Methods**: `SendMessage`, `MarkMessagesRead`, `ArchiveChat`, `TypingIndicator`
- **Unread Logic**: 
  - DB stores `IsRead` per `ChatMessage`
  - Backend DTO computes `UnreadCount` per chat for the requesting user: `Messages.Count(m => !m.IsRead && m.SenderId != currentUserId)`
  - Chat list UI shows `🔴 {count}` badge
  - When chat opens → call `MarkMessagesRead` → sets `IsRead = true`, `ReadAt = DateTime.UtcNow`
- **Archive**: `IsArchived = true` hides from main list, moves to `Archived` tab
- **Role Indicator**: Stored per `Chat.ContextRole` (Buyer/Seller/Neutral). Derived from who owns the linked listing vs who initiated chat.

### 🔔 Notifications (Broadcast + User-Specific)
- Admin pushes: `POST /api/admin/notifications/broadcast { title, body, source, deepLink }` → creates `Notification` rows with `UserId = NULL`, `IsBroadcast = true`
- App startup: `GET /api/notifications/unread` → returns both broadcast & user-specific where `IsRead = false`
- Read state: `PATCH /api/notifications/{id}/read` → updates `IsRead`
- **UI Hook**: On app resume → poll `/notifications/unread` once. If `count > 0` → show banner or badge.

### 🗂️ What’s Deliberately Omitted (V1 Scope)
- ❌ Orders/Payments → handled manually via contact/chat
- ❌ Multi-image galleries → `PrimaryImageUrl` only
- ❌ Real-time typing → placeholder in SignalR hub, optional V1.1
- ❌ Complex search → `?query=` text filter + category chip
- ❌ Push tokens → stored in `UserPreferences` or separate table when FCM is wired

### 🛠️ EF Core Configuration Tips
```csharp
// In OnModelCreating:
modelBuilder.Entity<UserPreference>().HasOne(p => p.User).WithOne(u => u.Preferences).HasForeignKey<UserPreference>(p => p.UserId);
modelBuilder.Entity<SavedListing>().HasKey(sl => new { sl.UserId, sl.ListingId });
modelBuilder.Entity<Notification>().HasIndex(n => new { n.UserId, n.IsRead }); // Fast unread queries
modelBuilder.Entity<ChatMessage>().HasIndex(m => new { m.ChatId, m.SentAt }); // Chat timeline performance
```

---

This schema is **lean, relational, SignalR-ready, and explicitly scoped** for your V1. Every model maps cleanly to Flutter, supports your admin/support roles, and leaves explicit hooks for AI enrichment later.


---