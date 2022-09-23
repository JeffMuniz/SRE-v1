
import {
 Action, Location, createBrowserHistory,
} from 'history';
import {observer} from 'mobx-react-lite';
import {
 Redirect, Route, Router, Switch,
} from 'react-router-dom';
import {ROUTES} from '~/consts';
import {
  AcquirersPage,
 BankAccountPage, CPFConfirmationPage, ConfirmationCodePage, CongratsPage, ContactInfoPage, DataConfirmationPage, EstablishmentDataPage, FeesAndTariffsPage, HomePage, IdentityConfirmationPage, NameConfirmationPage, PATQuestionsPage,
} from '~/pages';
import ContactPreferencePage from '~/pages/contact-preference/contact-preference.page';
import {Header} from '~/partials';
import Footer from '~/partials/footer/footer.partial';
import {AppDataStore, CartDataStore} from '~/stores';
import {GlobalStyles, ThemeProvider} from '~/theme';
import {PageWrapper, Wrapper} from './App.styled';

const handleNavigation = (location: Location, action: Action) => {
  AppDataStore.handleNavigationUpdate({
    action,
    location,
  });
};

const history = createBrowserHistory();
history.listen(handleNavigation);
handleNavigation(history.location, history.action);

const App = () => (
  <Wrapper>
    <GlobalStyles />
    <ThemeProvider>
      {CartDataStore.isHydrated && (
        <Router history={history}>
          <PageWrapper>
            <Switch>
              <Route
                exact
                path={ROUTES.HOME}
                component={HomePage}
              />
              <Route
                path={ROUTES.NAME_CONFIRMATION}
                component={NameConfirmationPage}
              />
              <Route
                path={ROUTES.CPF_CONFIRMATION}
                component={CPFConfirmationPage}
              />
              <Route
                path={ROUTES.CONTACT_INFO}
                component={ContactInfoPage}
              />
              <Route
                path={ROUTES.CONTACT_PREFERENCE}
                component={ContactPreferencePage}
              />
              <Route
                path={ROUTES.CONFIRMATION_CODE}
                component={ConfirmationCodePage}
              />
              <Route
                path={ROUTES.IDENTITY_CONFIRMATION}
                component={IdentityConfirmationPage}
              />
              <Route
                path={ROUTES.ESTABLISHMENT_DATA}
                component={EstablishmentDataPage}
              />
              <Route
                path={`${ROUTES.PAT_QUESTIONS}/:question`}
                component={PATQuestionsPage}
              />
              <Route
                path={[
                  ROUTES.ACQUIRERS,
                  `${ROUTES.ACQUIRERS}/:acquirer`,
                ]}
                component={AcquirersPage}
              />
              <Route
                path={ROUTES.FEES_AND_TARIFFS}
                component={FeesAndTariffsPage}
              />
              <Route
                path={ROUTES.BANK_ACCOUNT}
                component={BankAccountPage}
              />
              <Route
                path={ROUTES.CONGRATS}
                component={CongratsPage}
              />
              <Route
                path={ROUTES.DATA_CONFIRMATION}
                component={DataConfirmationPage}
              />

              {/* Redirections START */}
              <Redirect
                from={ROUTES.PAT_QUESTIONS}
                to={`${ROUTES.PAT_QUESTIONS}/vr-acceptance`}
              />
              <Redirect to={ROUTES.HOME} />
              {/* Redirections END */}
            </Switch>
          </PageWrapper>
          <Footer />
          <Header />
        </Router>
      )}
    </ThemeProvider>
  </Wrapper>
);

export default observer(App);
