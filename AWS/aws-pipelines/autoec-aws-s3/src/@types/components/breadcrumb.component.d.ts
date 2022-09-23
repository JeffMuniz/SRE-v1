
declare namespace Breadcrumb {

  type Step = {
    label: string;
    isActive: boolean;
  }

  type Props = {
    steps: Array<Step>;
  };
}
