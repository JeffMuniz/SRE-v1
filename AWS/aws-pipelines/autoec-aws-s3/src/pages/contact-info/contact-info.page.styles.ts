
import styled from 'styled-components';
import {BaseLabel, PageSubtitle} from '~/styled';

export const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
  flex-grow: 1;
`;

export const CIPageSubtitle = styled(PageSubtitle)`
  margin-bottom: 5px;
`;

export const Info = styled(BaseLabel)`
  margin-bottom: 30px;
`;
