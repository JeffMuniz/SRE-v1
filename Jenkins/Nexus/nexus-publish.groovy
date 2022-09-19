                        




                        stage("Publish Artifact - Nexus") {
                    when {
                      expression { SHOUD_STORE_ARTIFACT == 'true' }
                    }
                    steps {
                        withMaven( maven: 'maven-3', jdk: 'jdk11-open'){
                            sh "./mvnw clean install -DskipTests"
                        }
                        script {
                            pom = readMavenPom file: "pom.xml";
                            filesByGlob = findFiles(glob: "target/*.${pom.packaging}");
                            echo "${filesByGlob[0].name} ${filesByGlob[0].path} ${filesByGlob[0].directory} ${filesByGlob[0].length} ${filesByGlob[0].lastModified}"
                            artifactPath = filesByGlob[0].path;
                            artifactExists = fileExists artifactPath;
                            if(artifactExists) {
                                echo "*** Arquivo: ${artifactPath}, group: ${pom.groupId}, packaging: ${pom.packaging}, version ${pom.version}";
                                nexusArtifactUploader(
                                    nexusVersion: NEXUS_VERSION,
                                    protocol: NEXUS_PROTOCOL,
                                    nexusUrl: NEXUS_URL,
                                    groupId: pom.groupId,
                                    version: pom.version,
                                    repository: NEXUS_REPOSITORY,
                                    credentialsId: NEXUS_CREDENTIAL_ID,
                                   artifacts: [
                                        [artifactId: pom.artifactId, classifier: '', file: artifactPath,type: 'jar']
                                    ]
                                );
                            } else {
                                error "*** Arquivo: ${artifactPath}, não encontrado";
                            }
                        }
                    }
                }

            }





                        