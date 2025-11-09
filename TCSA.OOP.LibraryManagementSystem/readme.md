-------------------------
-- C# Access Modifiers --
-------------------------

➡️ Public: It's the most accessible modifier, allowing members to be accessed from any part of the code in the same assembly or from other assemblies. It ensures that the member is available to all.

➡️ Private: It's the most restrictive modifier, limiting access to the containing class only. Members marked as private cannot be accessed from outside the class, ensuring full encapsulation of the class's implementation details.

➡️ Protected: Allows access within the containing class and any class that inherits from it. This modifier is useful when you want derived classes to access certain members but still restrict access from the outside.

➡️ Internal: Limits access to the current assembly, meaning that any type or member marked as internal can only be accessed by other types within the same project but not by other assemblies.

➡️ Protected Internal: It's a combination of protected and internal, allowing access either from within the same assembly or from a derived class in another assembly. It offers flexibility when sharing access across inheritance hierarchies and assemblies.

➡️ Private protected: It's the most restrictive of all inheritance-access modifiers, allowing access only from within the containing class or types derived from the class but only within the same assembly. This ensures that members are not exposed to derived classes from other assemblies.

-------------------------
---- Best Practices -----
-------------------------

➡️ Default to the Least Permissive Access: Start with the most restrictive access level (private) and only increase accessibility when necessary. This minimizes unintended interactions and enhances security.

➡️ Use private for Implementation Details: Keep fields and helper methods private unless there’s a clear need for broader access.

➡️ Expose Only Necessary Members: Public members should represent the intended interface of the class. Avoid exposing internal workings unless required for extension or interaction.

➡️ Leverage internal for Assembly-Wide Access: Use internal when members need to be accessible across multiple classes within the same assembly but should remain hidden from external assemblies.

➡️ Document Access Levels: Clearly document the intended use and access levels of class members to aid maintainability and clarity for other developers.

NOTES:
If you don't explicitly declare an access modifier in C#, the default is private for members (fields, methods, properties) inside a class. This means that these members will only be accessible from within the same class or struct. For top-level types (such as classes, interfaces, or structs), the default access modifier is internal, making the type accessible only within the same assembly.