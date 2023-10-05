```mermaid
erDiagram
    Student ||--o{ class : takes
    class ||--o{ course : has
    grade ||--o{ Student : has
    grade ||--o{ course : has
    grade ||--o{ class : has
    
    
```