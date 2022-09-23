
import {FC, useCallback} from 'react';
import {observer} from 'mobx-react-lite';
import {useHistory} from 'react-router-dom';
import {ROUTES} from '~/consts';
import {PATQuestionsPageStore} from '../pat-questions.page.store';
import {
 Label, RMPageSubtitle, TryAgainButton, Wrapper,
} from './requirements-modal.styles';

const RequirementsModal: FC = () => {

  const history = useHistory();

  const hide = () => {
    PATQuestionsPageStore.setState(state => {
      state.showRequirementsModal = false;
    });
  };

  const handleBackgroundClick = useCallback(() => {
    hide();
  }, [ ]);

  const handleTryAgainButtonClick = useCallback(() => {
    hide();
    setTimeout(() => {
      history.push(ROUTES.PAT_QUESTIONS);
    }, 600);
  }, [ history ]);

  const {showRequirementsModal} = PATQuestionsPageStore.state;

  return (
    <Wrapper
      show={showRequirementsModal}
      onBackgroundClick={handleBackgroundClick}
    >
      <RMPageSubtitle>
        Ops!
      </RMPageSubtitle>
      <Label>
        É necessário selecionar Card Refeição e/ou Card Alimentação.
      </Label>
      <TryAgainButton onClick={handleTryAgainButtonClick}>
        tentar novamente
      </TryAgainButton>
    </Wrapper>
  );
};

export default observer(RequirementsModal);
