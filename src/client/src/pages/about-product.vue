<script>
export default {
  layout: "landing"
};
</script>
<template>
  <section class="about">
    <img class="about__bg" src="@/assets/footer-right.svg" alt="" />
    <h3 class="about__title">More About Our Product</h3>
    <div class="about__cta">
      <p>
        Defining tasks is an important step to convert a prototype into the
        live, working product. In order to do so, all the steps needed to design
        and develop this product is divided and grouped into the minimal tasks
        which helps to create a collaborative workspace. This mission is
        generally assigned to the people with Project Manager, Business Analyst
        or Solution Architect roles.
      </p>
      <p>
        One of the challenges these people come across are estimating the time
        and cost of the project before design & development. For making proper
        estimations, people with technical backgrounds and project management
        experiences needed to come up with common decision, which means it needs
        teamwork and other peoples' work hours. This also comes with another
        problem, since people might not share a common decision, they have to
        spend much more time and effort to persuade each other.
      </p>
      <p>
        In order to achieve this mission much faster and simpler, while PM is
        creating tasks and their estimations, it can be assisted with similar
        previous real-time estimations and results. What we are aiming with
        <strong>MEGA </strong>
        is showing this similar experiences from before and make suggestions
        with all this data combined. Since it uses real-life data, estimations
        would be much more realistic.
      </p>
    </div>
  </section>
</template>

<style lang="scss" scoped>
.about {
  position: relative;
  max-width: 1157px;
  margin-left: auto;
  margin-right: auto;
  &__bg {
    position: absolute;
    top: -200px;
    transform: rotate(180deg);
    left: 0;
    display: block;
  }

  &__cta {
    color: #0b2138;
    font-size: 18px;
    position: relative;
    z-index: 120;
    background: #fff;
    box-shadow: 17px 25px 47px rgba(8, 25, 43, 0.33);
    padding: 20px;
    margin: 32px;
    border-radius: 4px;
  }

  margin-top: 200px;
  &__title {
    color: #fff;
    position: relative;
    z-index: 100;
    text-align: center;
    letter-spacing: 4px;
    font-size: 40px;
    margin-top: 32px;
  }
}
</style>
