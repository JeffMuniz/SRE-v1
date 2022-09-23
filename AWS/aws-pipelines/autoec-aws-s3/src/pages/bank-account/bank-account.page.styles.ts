
import styled from 'styled-components';
import {BaseLabel, PageSubtitle} from '~/styled';

export const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
  flex-grow: 1;
`;

export const BAPageSubtitle = styled(PageSubtitle)`
  margin-bottom: 10px;
`;

export const Info = styled(BaseLabel)`
  display: block;
  margin-bottom: 20px;
`;
