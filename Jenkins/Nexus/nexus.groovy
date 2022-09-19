-
repo:nexusrepo/org/jetbrains/kotlin/kotlin-compiler-embeddable/1.3.71/kotlin-compiler-embeddable-1.3.71.pom]
repo:nexusrepo/org/jetbrains/intellij/deps/trove4j/1.0.20181211/trove4j-1.0.20181211.pom]


repo:maven-releases/org/jetbrains/kotlin/kotlin-compiler-embeddable/1.3.71/kotlin-compiler-embeddable-1.3.71.pom]
repo:maven-releases/org/jetbrains/kotlin/kotlin-script-runtime/1.3.71/kotlin-script-runtime-1.3.71.pom]
repo:maven-releases/org/jetbrains/intellij/deps/trove4j/1.0.20181211/trove4j-1.0.20181211.pom]
-



Could not resolve compiler classpath. Check if Kotlin Gradle plugin repository is configured in root project


: Build failed with an exception.

* What went wrong:
Could not determine the dependencies of task ':compileKotlin'.
> Could not resolve all dependencies for configuration ':detachedConfiguration3'.
   > Could not resolve org.springframework.boot:spring-boot-dependencies:2.2.13.RELEASE.
     Required by:
         project :
      > Could not resolve org.springframework.boot:spring-boot-dependencies:2.2.13.RELEASE.
         > Could not get resource 'https://nexus-qa.macpreprod.com/repository/maven-central/org/springframework/boot/spring-boot-dependencies/2.2.13.RELEASE/spring-boot-dependencies-2.2.13.RELEASE.pom'.
            > Could not HEAD 'https://nexus-qa.macpreprod.com/repository/maven-central/org/springframework/boot/spring-boot-dependencies/2.2.13.RELEASE/spring-boot-dependencies-2.2.13.RELEASE.pom'.
               > Read timed out

* Try: