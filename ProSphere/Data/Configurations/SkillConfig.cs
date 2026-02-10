using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProSphere.Domain.Entities;

namespace ProSphere.Data.Configurations
{
    public class SkillConfig : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasData(
                // Programming Languages
                new Skill { Id = 1, Name = "C" },
                new Skill { Id = 2, Name = "C++" },
                new Skill { Id = 3, Name = "C#" },
                new Skill { Id = 4, Name = "Java" },
                new Skill { Id = 5, Name = "Python" },
                new Skill { Id = 6, Name = "JavaScript" },
                new Skill { Id = 7, Name = "TypeScript" },
                new Skill { Id = 8, Name = "Go" },
                new Skill { Id = 9, Name = "Rust" },
                new Skill { Id = 10, Name = "PHP" },
                new Skill { Id = 11, Name = "Ruby" },
                new Skill { Id = 12, Name = "Kotlin" },
                new Skill { Id = 13, Name = "Swift" },
                new Skill { Id = 14, Name = "Objective-C" },
                new Skill { Id = 15, Name = "Dart" },
                new Skill { Id = 16, Name = "Scala" },
                new Skill { Id = 17, Name = "R" },
                new Skill { Id = 18, Name = "MATLAB" },
                new Skill { Id = 19, Name = "Groovy" },
                new Skill { Id = 20, Name = "Assembly" },
                new Skill { Id = 21, Name = "Bash" },
                new Skill { Id = 22, Name = "PowerShell" },
                new Skill { Id = 23, Name = "Perl" },
                new Skill { Id = 24, Name = "Lua" },
                new Skill { Id = 25, Name = "Haskell" },
                new Skill { Id = 26, Name = "Elixir" },
                new Skill { Id = 27, Name = "Julia" },
                new Skill { Id = 28, Name = "COBOL" },
                new Skill { Id = 29, Name = "Fortran" },

                // Frontend Core
                new Skill { Id = 30, Name = "HTML" },
                new Skill { Id = 31, Name = "HTML5" },
                new Skill { Id = 32, Name = "CSS" },
                new Skill { Id = 33, Name = "CSS3" },
                new Skill { Id = 34, Name = "SCSS" },
                new Skill { Id = 35, Name = "SASS" },
                new Skill { Id = 36, Name = "LESS" },
                new Skill { Id = 37, Name = "Responsive Design" },
                new Skill { Id = 38, Name = "Flexbox" },
                new Skill { Id = 39, Name = "CSS Grid" },
                new Skill { Id = 40, Name = "Web Accessibility" },
                new Skill { Id = 41, Name = "Cross-Browser Compatibility" },

                // CSS Frameworks & UI
                new Skill { Id = 42, Name = "Bootstrap" },
                new Skill { Id = 43, Name = "Tailwind CSS" },
                new Skill { Id = 44, Name = "Material UI" },
                new Skill { Id = 45, Name = "Ant Design" },
                new Skill { Id = 46, Name = "Chakra UI" },
                new Skill { Id = 47, Name = "Bulma" },
                new Skill { Id = 48, Name = "Foundation" },
                new Skill { Id = 49, Name = "Semantic UI" },
                new Skill { Id = 50, Name = "ShadCN UI" },
                new Skill { Id = 51, Name = "Styled Components" },
                new Skill { Id = 52, Name = "Emotion" },

                // Frontend Frameworks
                new Skill { Id = 53, Name = "React" },
                new Skill { Id = 54, Name = "React Native" },
                new Skill { Id = 55, Name = "Next.js" },
                new Skill { Id = 56, Name = "Vue.js" },
                new Skill { Id = 57, Name = "Nuxt.js" },
                new Skill { Id = 58, Name = "Angular" },
                new Skill { Id = 59, Name = "Svelte" },
                new Skill { Id = 60, Name = "SvelteKit" },
                new Skill { Id = 61, Name = "jQuery" },
                new Skill { Id = 62, Name = "Redux" },
                new Skill { Id = 63, Name = "Zustand" },
                new Skill { Id = 64, Name = "MobX" },
                new Skill { Id = 65, Name = "RxJS" },
                new Skill { Id = 66, Name = "Alpine.js" },

                // Backend Frameworks
                new Skill { Id = 67, Name = "ASP.NET Core" },
                new Skill { Id = 68, Name = "ASP.NET MVC" },
                new Skill { Id = 69, Name = "ASP.NET Web API" },
                new Skill { Id = 70, Name = "Blazor" },
                new Skill { Id = 71, Name = "Node.js" },
                new Skill { Id = 72, Name = "Express.js" },
                new Skill { Id = 73, Name = "NestJS" },
                new Skill { Id = 74, Name = "FastAPI" },
                new Skill { Id = 75, Name = "Django" },
                new Skill { Id = 76, Name = "Flask" },
                new Skill { Id = 77, Name = "Spring Boot" },
                new Skill { Id = 78, Name = "Laravel" },
                new Skill { Id = 79, Name = "Symfony" },
                new Skill { Id = 80, Name = "Ruby on Rails" },
                new Skill { Id = 81, Name = "Phoenix" },
                new Skill { Id = 82, Name = "Ktor" },
                new Skill { Id = 83, Name = "Gin" },
                new Skill { Id = 84, Name = "Fiber" },

                // Databases SQL
                new Skill { Id = 85, Name = "SQL Server" },
                new Skill { Id = 86, Name = "PostgreSQL" },
                new Skill { Id = 87, Name = "MySQL" },
                new Skill { Id = 88, Name = "MariaDB" },
                new Skill { Id = 89, Name = "SQLite" },
                new Skill { Id = 90, Name = "Oracle Database" },
                new Skill { Id = 91, Name = "CockroachDB" },

                // Databases NoSQL
                new Skill { Id = 92, Name = "MongoDB" },
                new Skill { Id = 93, Name = "Redis" },
                new Skill { Id = 94, Name = "Cassandra" },
                new Skill { Id = 95, Name = "DynamoDB" },
                new Skill { Id = 96, Name = "Firebase Firestore" },
                new Skill { Id = 97, Name = "CouchDB" },
                new Skill { Id = 98, Name = "Neo4j" },
                new Skill { Id = 99, Name = "ArangoDB" },
                new Skill { Id = 100, Name = "ElasticSearch" },

                // Cloud Platforms
                new Skill { Id = 101, Name = "Microsoft Azure" },
                new Skill { Id = 102, Name = "AWS" },
                new Skill { Id = 103, Name = "Google Cloud Platform" },
                new Skill { Id = 104, Name = "Azure App Service" },
                new Skill { Id = 105, Name = "Azure Functions" },
                new Skill { Id = 106, Name = "AWS EC2" },
                new Skill { Id = 107, Name = "AWS Lambda" },
                new Skill { Id = 108, Name = "AWS S3" },
                new Skill { Id = 109, Name = "Google Cloud Functions" },
                new Skill { Id = 110, Name = "Cloudflare" },
                new Skill { Id = 111, Name = "Vercel" },
                new Skill { Id = 112, Name = "Netlify" },

                // DevOps & Infrastructure
                new Skill { Id = 113, Name = "Docker" },
                new Skill { Id = 114, Name = "Docker Compose" },
                new Skill { Id = 115, Name = "Kubernetes" },
                new Skill { Id = 116, Name = "Helm" },
                new Skill { Id = 117, Name = "Terraform" },
                new Skill { Id = 118, Name = "Ansible" },
                new Skill { Id = 119, Name = "CI/CD" },
                new Skill { Id = 120, Name = "GitHub Actions" },
                new Skill { Id = 121, Name = "GitLab CI" },
                new Skill { Id = 122, Name = "Azure DevOps" },
                new Skill { Id = 123, Name = "Jenkins" },
                new Skill { Id = 124, Name = "ArgoCD" },
                new Skill { Id = 125, Name = "Nginx" },
                new Skill { Id = 126, Name = "Apache" },

                // Security & Auth
                new Skill { Id = 127, Name = "JWT" },
                new Skill { Id = 128, Name = "OAuth 2.0" },
                new Skill { Id = 129, Name = "OpenID Connect" },
                new Skill { Id = 130, Name = "ASP.NET Identity" },
                new Skill { Id = 131, Name = "IdentityServer" },
                new Skill { Id = 132, Name = "Auth0" },
                new Skill { Id = 133, Name = "Keycloak" },
                new Skill { Id = 134, Name = "OWASP" },
                new Skill { Id = 135, Name = "Rate Limiting" },
                new Skill { Id = 136, Name = "Encryption" },
                new Skill { Id = 137, Name = "Hashing" },

                // APIs & Communication
                new Skill { Id = 138, Name = "REST API" },
                new Skill { Id = 139, Name = "GraphQL" },
                new Skill { Id = 140, Name = "gRPC" },
                new Skill { Id = 141, Name = "WebSockets" },
                new Skill { Id = 142, Name = "SignalR" },
                new Skill { Id = 143, Name = "RabbitMQ" },
                new Skill { Id = 144, Name = "Kafka" },
                new Skill { Id = 145, Name = "Azure Service Bus" },
                new Skill { Id = 146, Name = "AWS SQS" },
                new Skill { Id = 147, Name = "MQTT" },

                // Testing
                new Skill { Id = 148, Name = "Unit Testing" },
                new Skill { Id = 149, Name = "Integration Testing" },
                new Skill { Id = 150, Name = "End-to-End Testing" },
                new Skill { Id = 151, Name = "xUnit" },
                new Skill { Id = 152, Name = "NUnit" },
                new Skill { Id = 153, Name = "MSTest" },
                new Skill { Id = 154, Name = "Jest" },
                new Skill { Id = 155, Name = "Mocha" },
                new Skill { Id = 156, Name = "Chai" },
                new Skill { Id = 157, Name = "Cypress" },
                new Skill { Id = 158, Name = "Playwright" },
                new Skill { Id = 159, Name = "Selenium" },
                new Skill { Id = 160, Name = "Postman" },
                new Skill { Id = 161, Name = "Swagger" },

                // Architecture & Patterns
                new Skill { Id = 162, Name = "Clean Architecture" },
                new Skill { Id = 163, Name = "Onion Architecture" },
                new Skill { Id = 164, Name = "Hexagonal Architecture" },
                new Skill { Id = 165, Name = "Vertical Slice Architecture" },
                new Skill { Id = 166, Name = "DDD" },
                new Skill { Id = 167, Name = "CQRS" },
                new Skill { Id = 168, Name = "Event Sourcing" },
                new Skill { Id = 169, Name = "Repository Pattern" },
                new Skill { Id = 170, Name = "Unit of Work" },
                new Skill { Id = 171, Name = "SOLID Principles" },
                new Skill { Id = 172, Name = "Microservices" },
                new Skill { Id = 173, Name = "Monolith" },

                // Data & Performance
                new Skill { Id = 174, Name = "LINQ" },
                new Skill { Id = 175, Name = "Entity Framework Core" },
                new Skill { Id = 176, Name = "Dapper" },
                new Skill { Id = 177, Name = "Caching" },
                new Skill { Id = 178, Name = "Redis Cache" },
                new Skill { Id = 179, Name = "In-Memory Cache" },
                new Skill { Id = 180, Name = "Lazy Loading" },
                new Skill { Id = 181, Name = "Eager Loading" },
                new Skill { Id = 182, Name = "Indexing" },
                new Skill { Id = 183, Name = "Query Optimization" },

                // Mobile
                new Skill { Id = 184, Name = "Flutter" },
                new Skill { Id = 185, Name = "React Native" },
                new Skill { Id = 186, Name = "Xamarin" },
                new Skill { Id = 187, Name = ".NET MAUI" },
                new Skill { Id = 188, Name = "Android Development" },
                new Skill { Id = 189, Name = "iOS Development" },
                new Skill { Id = 190, Name = "SwiftUI" },
                new Skill { Id = 191, Name = "Jetpack Compose" },

                // Game & Graphics
                new Skill { Id = 192, Name = "Unity" },
                new Skill { Id = 193, Name = "Unreal Engine" },
                new Skill { Id = 194, Name = "Godot" },
                new Skill { Id = 195, Name = "Cocos2d" },
                new Skill { Id = 196, Name = "OpenGL" },
                new Skill { Id = 197, Name = "WebGL" },
                new Skill { Id = 198, Name = "Three.js" },

                // AI & Data
                new Skill { Id = 199, Name = "Machine Learning" },
                new Skill { Id = 200, Name = "Deep Learning" },
                new Skill { Id = 201, Name = "TensorFlow" },
                new Skill { Id = 202, Name = "PyTorch" },
                new Skill { Id = 203, Name = "Scikit-learn" },
                new Skill { Id = 204, Name = "OpenAI API" },
                new Skill { Id = 205, Name = "NLP" },
                new Skill { Id = 206, Name = "Computer Vision" },
                new Skill { Id = 207, Name = "Data Analysis" },
                new Skill { Id = 208, Name = "Pandas" },
                new Skill { Id = 209, Name = "NumPy" },

                // Tools & Platforms
                new Skill { Id = 210, Name = "Git" },
                new Skill { Id = 211, Name = "GitHub" },
                new Skill { Id = 212, Name = "GitLab" },
                new Skill { Id = 213, Name = "Bitbucket" },
                new Skill { Id = 214, Name = "Visual Studio" },
                new Skill { Id = 215, Name = "Visual Studio Code" },
                new Skill { Id = 216, Name = "Rider" },
                new Skill { Id = 217, Name = "IntelliJ IDEA" },
                new Skill { Id = 218, Name = "Postman" },
                new Skill { Id = 219, Name = "Swagger" },
                new Skill { Id = 220, Name = "Figma" },
                new Skill { Id = 221, Name = "Jira" },
                new Skill { Id = 222, Name = "Trello" },
                new Skill { Id = 223, Name = "Notion" },

                // Package Managers
                new Skill { Id = 224, Name = "npm" },
                new Skill { Id = 225, Name = "yarn" },
                new Skill { Id = 226, Name = "pnpm" },
                new Skill { Id = 227, Name = "NuGet" },
                new Skill { Id = 228, Name = "pip" },
                new Skill { Id = 229, Name = "Poetry" },
                new Skill { Id = 230, Name = "Composer" },
                new Skill { Id = 231, Name = "Maven" },
                new Skill { Id = 232, Name = "Gradle" },

                // Observability & Logging
                new Skill { Id = 233, Name = "Serilog" },
                new Skill { Id = 234, Name = "NLog" },
                new Skill { Id = 235, Name = "Log4Net" },
                new Skill { Id = 236, Name = "ELK Stack" },
                new Skill { Id = 237, Name = "Prometheus" },
                new Skill { Id = 238, Name = "Grafana" },
                new Skill { Id = 239, Name = "Grafana" },
                new Skill { Id = 240, Name = "OpenTelemetry" },
                new Skill { Id = 241, Name = "Application Insights" }
            );
        }
    }
}
