<script>
export default {
  layout: "landing",
  methods: {
    handleScroll() {
      window.scrollTo({
        top: 520,
        behavior: "smooth"
      });
    }
  }
};
</script>

<template>
  <div>
    <section class="hero">
      <div class="cta">
        <h2 class="cta__title">Make Estimation Great Again!</h2>
        <p class="cta__p">
          Browse and discover millions of vectors, photos, PSDs, icons, and free
          tutorials. Enjoy creating without limits
        </p>
        <span class="cta__link" @click="handleScroll">Explore</span>
      </div>
      <div class="bg">
        <img class="bg__img2" src="@/assets/the_man.svg" alt="" />
        <img class="bg__img" src="@/assets/home-team.svg" alt="" />
      </div>
    </section>

    <section class="cards">
      <h3 class="cards__title">Inspiration, creativity and design</h3>
      <div class="cards_list">
        <div class="card">
          <h3 class="card__title">Motivation</h3>
          <img class="card__icon" src="@/assets/stopwatch.svg" alt="" />
          <p class="card__p">
            Lorem, ipsum dolor sit amet consectetur adipisicing elit.
          </p>
        </div>
        <div class="card">
          <h3 class="card__title">Colloboration</h3>
          <img class="card__icon" src="@/assets/networking.svg" alt="" />
          <p class="card__p">
            Lorem, ipsum dolor sit amet consectetur adipisicing elit.
          </p>
        </div>
        <div class="card">
          <h3 class="card__title">Motivation</h3>
          <img class="card__icon" src="@/assets/calendar.svg" alt="" />
          <p class="card__p">
            Lorem, ipsum dolor sit amet consectetur adipisicing elit.
          </p>
        </div>
      </div>
    </section>

    <section class="definition">
      <h3 class="definition__title">What Is MEGA?</h3>
      <p class="definition__p">
        <strong>MEGA</strong> is an Issue Time Estimation Assistant which helps
        Project Managers to estimate the time needed for tasks by referencing
        from previous experiences. JAPE has been developed in the hackathon
        challenge, OSF Hackers 2019 by the team Gang of Four.
      </p>
      <img class="definition__bg" src="@/assets/bg.svg" />
    </section>
    <section class="introduction">
      <h3 class="introduction__title">More About Our Product</h3>
      <div class="introduction__cta">
        <p>
          Defining tasks is an important step to convert a prototype into the
          live, working product. In order to do so, all the steps needed to
          design and develop this product is divided and grouped into the
          minimal tasks which helps to create a collaborative workspace. This
          mission is generally assigned to the people with Project Manager,
          Business Analyst or Solution Architect roles.
        </p>
        <p>
          One of the challenges these people come across are estimating the time
          and cost of the project before design & development. For making proper
          estimations, people with technical backgrounds and project management
          experiences needed to come up with common decision, which means it
          needs teamwork and other peoples' work hours. This also comes with
          another problem, since people might not share a common decision, they
          have to spend much more time and effort to persuade each other.
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
  </div>
</template>

<style lang="scss" scoped>
.hero {
  max-width: 1157px;
  margin-left: auto;
  margin-right: auto;
  position: relative;
  z-index: 2;
}
.cta {
  max-width: 700px;
  padding: 120px 0;
  position: relative;
  z-index: 2;
  &__title {
    margin: 20px 0;
    padding: 0;
    color: #374957;
    font-weight: bold;
    line-height: 1.4;
    font-size: 46px;
  }
  &__p {
    font-size: 22px;
    color: #5f7d95;
    line-height: 1.6;
    position: relative;
  }
  &__link {
    box-shadow: inset 0 0 0 2px #202095;
    color: #202095;
    -webkit-transition: box-shadow 0.3s cubic-bezier(0.25, 0.46, 0.45, 0.94);
    -moz-transition: box-shadow 0.3s cubic-bezier(0.25, 0.46, 0.45, 0.94);
    transition: box-shadow 0.3s cubic-bezier(0.25, 0.46, 0.45, 0.94);
    padding: 10px 20px;
    max-width: 120px;
    cursor: pointer;
    margin-top: 32px;
    font-size: 20px;
  }
}

.bg {
  &__img {
    position: absolute;
    top: 100px;
    display: block;
    left: -100px;
    z-index: 1;
  }
  &__img2 {
    position: absolute;
    top: 0;
    display: block;
    right: 0;
    z-index: 3;
  }
}

.cards {
  width: 100%;
  max-width: 1157px;
  margin-left: auto;
  margin-right: auto;
  position: relative;
  .cards__title {
    font-size: 42px;
    margin-bottom: 4rem;
    position: relative;
    z-index: 4;
    text-align: center;
    color: #fff;
    text-shadow: 1px 2px #202095;
  }

  &_list {
    display: grid;
    grid-template-columns: repeat(3, 1fr);
    grid-gap: 0;
    position: relative;
    z-index: 5;
    margin-top: 2rem;
  }
}
.card {
  position: relative;
  z-index: 6;
  background: #fff;
  padding: 20px;
  box-shadow: 17px 25px 47px rgba(8, 25, 43, 0.33);
  &__title {
    text-align: center;
    font-size: 24px;
    font-weight: 400;
    margin-bottom: 20px;
  }
  &__p {
    position: relative;
    font-size: 18px;
    z-index: 60;
  }
  &__icon {
    display: block;
    max-width: 100px;
    width: 100%;
    margin-left: auto;
    margin-right: auto;
    margin-bottom: 1rem;
  }
}
.definition {
  position: relative;
  max-width: 1157px;
  margin-left: auto;
  margin-right: auto;
  background: #0b2138;
  &__title {
    color: #fff;
    position: relative;
    z-index: 100;
    text-align: center;
    letter-spacing: 4px;
    font-size: 40px;
    margin-top: 32px;
  }
  &__bg {
    position: absolute;
    display: block;
    left: auto;
    right: auto;
    z-index: 120;
    top: 210px;
  }
  &__p {
    position: relative;
    z-index: 100;
    color: #374957;
    max-width: 700px;
    background: #fff;
    margin-left: auto;
    margin-right: auto;
    letter-spacing: 1px;
    border-radius: 4px;
    padding: 32px;
    font-size: 18px;
    margin-top: 32px;
  }
}

.introduction {
  position: relative;
  max-width: 1157px;
  margin-left: auto;
  margin-right: auto;

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
    color: #0b2138;
    position: relative;
    z-index: 100;
    text-align: center;
    letter-spacing: 4px;
    font-size: 40px;
    margin-top: 32px;
  }
}
</style>
