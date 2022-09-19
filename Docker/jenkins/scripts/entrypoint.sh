#! /bin/bash
# vim:set noexpandtab: # heredoc identado funciona apenas com tabs de verdade.

# Instala mais plugins se a variavel estiver populada.
if [[ -n ${PLUGINS} ]]; then
	install-plugins.sh ${PLUGINS}
fi

# Configuracoes basicas de seguranca e criacao de usuario padrao se JENKINS_ADMIN e JENKINS_PASSWORD estiverem populados.
if [[ -n ${JENKINS_ADMIN} ]] && [[ -n ${JENKINS_PASSWORD} ]] ; then
	export JAVA_OPTS="${JAVA_OPTS} -Djenkins.install.runSetupWizard=false"

	cat <<-EOF > /usr/share/jenkins/ref/init.groovy.d/security.groovy
		#!groovy

		import hudson.security.*
		import hudson.security.csrf.DefaultCrumbIssuer
		import jenkins.model.*
		import jenkins.security.s2m.AdminWhitelistRule

		def hudsonRealm					 = new HudsonPrivateSecurityRealm(false)
		def instance					 = Jenkins.getInstance()
		def jenkinsLocationConfiguration = JenkinsLocationConfiguration.get()
		def pass						 = System.getenv('JENKINS_PASSWORD')
		def strategy					 = new FullControlOnceLoggedInAuthorizationStrategy()
		def user						 = System.getenv('JENKINS_ADMIN')

		hudsonRealm.createAccount(user, pass)

		strategy.setAllowAnonymousRead(false)

		instance.setSecurityRealm(hudsonRealm)
		instance.setCrumbIssuer(new DefaultCrumbIssuer(true))
		instance.setAuthorizationStrategy(strategy)
		instance.save()

		Jenkins.instance.getInjector().getInstance(AdminWhitelistRule.class).setMasterKillSwitch(false)

		EOF
else
	# Remove o script caso JENKINS_PASSWORD ou JENKINS_ADMIN nao estiverem defindos, mantendo as configuracoes.
	rm -f /var/jenkins_home/init.groovy.d/security.groovy 2> /dev/null
fi

# Atualiza os plugins instalados durante o create (ou recreate) se a variavel UPDATE_PLUGINS for true.
if [[ ${UPDATE_PLUGINS} == true ]] ; then
	cat <<-EOF > /usr/share/jenkins/ref/init.groovy.d/update-plugins.groovy
		#!groovy

		jenkins.model.Jenkins.getInstance().getUpdateCenter().getSites().each { site ->
			site.updateDirectlyNow(hudson.model.DownloadService.signatureCheck)
		}

		hudson.model.DownloadService.Downloadable.all().each { downloadable ->
			downloadable.updateNow();
		}

		def plugins = jenkins.model.Jenkins.instance.pluginManager.activePlugins.findAll {
			it -> it.hasUpdate()
		}.collect {
			it -> it.getShortName()
		}

		println "Plugins to upgrade: \${plugins}"
		long count = 0

		jenkins.model.Jenkins.instance.pluginManager.install(plugins, false).each { f ->
			f.get()
			println "\${++count}/\${plugins.size()}.."
		}

		if(plugins.size() != 0 && count == plugins.size()) {
			jenkins.model.Jenkins.instance.restart()
		}

		EOF

else
	# Remove o script acima, nao atualizando os plugins durante o proximo create (ou recreate)
	rm -f /var/jenkins_home/init.groovy.d/update-plugins.groovy 2> /dev/null
fi

exec $@
