##Entity Component System (ECS)
	Although ECS is not ready for Unity yet, we can still benefit from the ideas behind ECS. The most important thing about ECS is how clearly it separates data (Component) from logic (System), how perfectly Component and System works with Scriptable Object (SO) and Interface, and how great flexibility this combination has given to this project.
	In this project, a Component is treated as a pure data container, and it may contain or be contained within SOs. In some cases, an SO is also treated exactly like a Component since SOs are great choices to be used as data containers. 