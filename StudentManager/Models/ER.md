```mermaid
erDiagram
    Student ||--o{ class : takes
    grade ||--o{ Student : has
    class }o--o{ course : has
    Education }|--o{ course : has
    grade ||--o{ course : has
    Education ||--o{ class : has
```